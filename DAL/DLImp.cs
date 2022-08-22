using DAL.DB;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using BO.Flights;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using GeoCoordinatePortable;
//using System.Activities;
//using System.Device.Location;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using sqlServer;

namespace DAL
{
    public class DLImp : IDL
    {
        

        #region flights
       
        private const string flightDetails = @"https://data-live.flightradar24.com/clickhandler/?version=1.5&flight=";
        public Dictionary<string, List<FlightInfoPartial>> GetCurrentFlights()
        {
            JObject AllFlightsData = null;
            string allURL = @"https://data-cloud.flightradar24.com/zones/fcgi/feed.js?faa=1&bounds=41.13,29.993,25.002,36.383&satellite=1&mlat=1&flarm=1&adsb=1&gnd=1&air=1&vehicles=1&estimated=1&maxage=14400&gliders=1&selected=2d1e1f33&ems=1&stats=1";

            Dictionary<string, List<FlightInfoPartial>> flightsDictionary = new Dictionary<string, List<FlightInfoPartial>>();

            List<FlightInfoPartial> Incoming = new List<FlightInfoPartial>();
            List<FlightInfoPartial> Outgoing = new List<FlightInfoPartial>();

            using (var webClient = new System.Net.WebClient())
            {
                HelperClass Helper = new HelperClass();
                var json = RequestDataSync(allURL);
                AllFlightsData = JObject.Parse(json);
                try
                {
                    foreach (var item in AllFlightsData)
                    {
                        var key = item.Key;
                        if (key == "full_count" || key == "version")
                            continue;
                        if (item.Value[11].ToString() == "TLV")
                            Outgoing.Add(new FlightInfoPartial
                            {
                                Source = item.Value[11].ToString(),
                                Destination = item.Value[12].ToString(),
                                SourceId = key,
                                Long = Convert.ToDouble(item.Value[2]),
                                Lat = Convert.ToDouble(item.Value[1]),
                                DateAndTime = Helper.GetDateTimeFromEpoch(Convert.ToDouble(item.Value[10])),
                                FlightCode = item.Value[13].ToString(),
                            });
                        else if (item.Value[12].ToString() == "TLV")
                            Incoming.Add(new FlightInfoPartial
                            {
                                Id = -1,
                                Source = item.Value[11].ToString(),
                                Destination = item.Value[12].ToString(),
                                SourceId = key,
                                Long = Convert.ToDouble(item.Value[2]),
                                Lat = Convert.ToDouble(item.Value[1]),
                                DateAndTime = Helper.GetDateTimeFromEpoch(Convert.ToDouble(item.Value[10])),
                                FlightCode = item.Value[13].ToString(),


                            });
                    }
                }
                catch (Exception e)
                {
                    Debug.Print(e.Message);
                }

                flightsDictionary.Add("Incoming", Incoming);
                flightsDictionary.Add("Outgoing", Outgoing);
            }
            return flightsDictionary;
        }



        private string RequestDataSync(string url)
        {
            using (var webClient = new System.Net.WebClient())
            {
                return webClient.DownloadString(url);
            }
        }

        public FlightDetail GetFlightData(string key)
        {
            string CurrentUrl =(string) flightDetails + key;
            FlightDetail currentFlight = null;
           
            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(CurrentUrl);
                try
                {
                    currentFlight = (FlightDetail)JsonConvert.DeserializeObject<FlightDetail>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }); 
                }catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }

            }

            return currentFlight;   
        }


        #endregion

        #region user
        public void AddUser(User u)
        {
            using (var db = new FlightContext())
            {
                db.Users.Add(u);
                db.SaveChanges();
            }
           
        }

        public void AddWatch(Watch w)
        {
            using (var db = new FlightContext())
            {
                db.Watches.Add(w);
                db.SaveChanges();
            }
        }

        public void UpdatePassword(User u, string newPassword)
        {
            using (var ctx = new FlightContext())
            {
                var user = ctx.Users.Find(u);
                if (user != null)
                {
                    ctx.Users.Remove(user);
                    user.Password = newPassword;
                    ctx.Users.Add(user);
                    ctx.SaveChanges();
                }

            }
        }

        public bool ExistUser(User u)
        {
            using (var ctx = new FlightContext())
            {
                if (ctx.Users.Find(u.UserId) != null)
                    return true;
                return false;
            }
        }

        public List<Watch> GetUserWatches(string userName, DateTime start, DateTime end)
        {
            using (var db = new FlightContext())
            {
                var l = (db.Watches.Where(w => w.UserName == userName && w.Date <= end && w.Date >= start)).ToList();
                l.Reverse();
                return l;
            }
        }
        #endregion

        #region holidays
        public string GetNextWeekHolidies()
        {
            string start = DateTime.Today.ToString("yyyy-MM-dd").Replace('/','-');
            string end = DateTime.Today.AddDays(7).ToString("yyyy-MM-dd").Replace('/', '-');
            string URL = @"https://www.hebcal.com/hebcal?v=1&cfg=json&maj=on&min=on&mod=on&start="+ start + "&end=" + end;
            using(var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(URL);
                HolidayRoot holidayRoot = JsonConvert.DeserializeObject<HolidayRoot>(json);
                
                if (holidayRoot.items.Count > 0)
                {
                    holidayRoot.items.RemoveAll(i => i.subcat == "fast");
                    holidayRoot.items.Sort();
                    return holidayRoot.items.First().title;
                }
                return "";
            }
        }
        #endregion

        #region weather
        public Dictionary<string,string> GetCurrentWeather(string lon, string lat)
        {
            Dictionary<string,string> result = new Dictionary<string,string>();

            string URL = @"https://openweathermap.org/data/2.5/onecall?lat=" + lat + "&lon=" + lon + "&units=metric&appid=439d4b804bc8187953eb36d2a8c26a02";
            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(URL);
                WeatherRoot weatherRoot = JsonConvert.DeserializeObject<WeatherRoot>(json);
                string temperature = weatherRoot.current.temp.ToString();
                string mainWeather = weatherRoot.current.weather.First().main;
                string shortDesc = weatherRoot.current.weather.First().description;

                result.Add("temperature", temperature);
                result.Add("mainWeather", mainWeather);
                result.Add("shortDesc", shortDesc);
                     
            }
            return result;
        }
        #endregion

        #region distance and time
        public Dictionary<string,string> GetLonLatOrigin(FlightDetail flight)
        {
            string lon = flight.airport.origin.position.longitude.ToString();
            string lat = flight.airport.origin.position.latitude.ToString();
            return new Dictionary<string, string>() { { "lon", lon }, { "lat", lat } };
        }

        public Dictionary<string, string> GetLonLatDestination(FlightDetail flight)
        {
            string lon = flight.airport.destination.position.longitude.ToString();
            string lat = flight.airport.destination.position.latitude.ToString();
            return new Dictionary<string, string>() { { "lon", lon }, { "lat", lat } };
        }

        public double GetDistance(FlightDetail flight)
        {
            Dictionary<string, string> origin = GetLonLatOrigin(flight);
            Dictionary<string, string> dest = GetLonLatDestination(flight);

            GeoCoordinate pin1 = new GeoCoordinate(Convert.ToDouble(origin["lat"]), Convert.ToDouble(origin["lon"]));
            GeoCoordinate pin2 = new GeoCoordinate(Convert.ToDouble(dest["lat"]), Convert.ToDouble(dest["lon"]));

            double distanceBetween = pin1.GetDistanceTo(pin2);

            return distanceBetween/1000;

        }

        public double GetRemainingDst(FlightDetail flight, FlightInfoPartial fip)
        {
            Dictionary<string, string> dest = GetLonLatDestination(flight);

            GeoCoordinate pin1 = new GeoCoordinate(Convert.ToDouble(fip.Lat), Convert.ToDouble(fip.Long));
            GeoCoordinate pin2 = new GeoCoordinate(Convert.ToDouble(dest["lat"]), Convert.ToDouble(dest["lon"]));

            double distanceBetween = pin1.GetDistanceTo(pin2);

            
            double tmp =  distanceBetween/1000; //km
            return tmp;
        }

        public TimeSpan GetTimeBetween(FlightDetail flight)
        {
            HelperClass helperClass = new HelperClass();
            DateTime origin = DateTime.UtcNow;
            DateTime dest = helperClass.GetDateTimeFromEpoch(flight.time.scheduled.arrival);

            TimeSpan res=  dest.Subtract(origin);
            return res;

        }

        #endregion

    }
}
