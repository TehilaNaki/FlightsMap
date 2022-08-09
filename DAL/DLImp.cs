using DAL.DB;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using BO.Flights;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace DAL
{
    public class DLImp : IDL
    {

        #region flights
        private const string allFlightsURL = @"https://data-cloud.flightradar24.com/zones/fcgi/feed.js?faa=1&bounds=53.203%2C44.17%2C-3.48%2C7.9&satellite=1&mlat=1&flarm=1&adsb=1&gnd=1&air=1&vehicles=1&estimated=1&maxage=14400&gliders=1&stats=1";
        private const string flightDetails = @"https://data-live.flightradar24.com/clickhandler/?version=1.5&flight=";

        public Dictionary<string, List<FlightInfoPartial>> GetCurrentFlights()
        {
            Dictionary<string, List<FlightInfoPartial>> result = new Dictionary<string, List<FlightInfoPartial>>();
            JObject allFlightData = null;
            //IList<string> keys = null;
            //IList<Object> values = null;

            List<FlightInfoPartial> incoming = new List<FlightInfoPartial>();
            List<FlightInfoPartial> outgoing = new List<FlightInfoPartial>();

            using(var webClient= new System.Net.WebClient()) 
            {
                var json = webClient.DownloadString(allFlightsURL);
                HelperClass Helper = new HelperClass();
                allFlightData=JObject.Parse(json);

                try
                {
                    foreach(var item in allFlightData)
                    {
                        var key = item.Key;
                        if (key == "full_count") continue;
                        if(key =="version") continue;
                        if (item.Value[11].ToString() == "TLV") outgoing.Add(
                            new FlightInfoPartial 
                            {   
                                Id = -1, 
                                Source = item.Value[11].ToString(), 
                                Destination = item.Value[12].ToString(), 
                                SourceId = key, 
                                Long = Convert.ToDouble(item.Value[1]), 
                                Lat = Convert.ToDouble(item.Value[2]), 
                                DateAndTime = Helper.GetDateTimeFromEpoch(Convert.ToDouble(item.Value[10])), 
                                FlightCode = item.Value[13].ToString()
                            });
                        if (item.Value[12].ToString() == "TLV") incoming.Add(
                              new FlightInfoPartial
                              {
                                  Id = -1,
                                  Source = item.Value[11].ToString(),
                                  Destination = item.Value[12].ToString(),
                                  SourceId = key,
                                  Long = Convert.ToDouble(item.Value[1]),
                                  Lat = Convert.ToDouble(item.Value[2]),
                                  DateAndTime = Helper.GetDateTimeFromEpoch(Convert.ToDouble(item.Value[10])),
                                  FlightCode = item.Value[13].ToString()
                              });
                    }
                }
                catch(Exception e)
                {
                    Debug.Print(e.Message);
                }
            }
            result.Add("Incoming", incoming);
            result.Add("Outgoing", outgoing);

            return result;

        }

         
            


        
        #endregion
        public void AddUser(User u)
        {
            using (var db = new FlightContext())
            {
                db.Users.Add(u);
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

        public string GetNextWeekHolidies()
        {
            string start = DateTime.Today.ToString("yyyy-MM-dd").Replace('/','-');
            string end = DateTime.Today.AddDays(7).ToString("yyyy-MM-dd").Replace('/', '-');
            string URL = @"https://www.hebcal.com/hebcal?v=1&cfg=json&maj=on&min=on&mod=on&start="+ start + "&end=" + end;
            HolidayRoot h = null;
            using(var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(URL);
                HolidayRoot holidayRoot = JsonConvert.DeserializeObject<HolidayRoot>(json);
                
                if (holidayRoot.items.Count > 0)
                {
                    holidayRoot.items.Sort();
                    return holidayRoot.items.First().title;
                }
                return "";
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
               return (List<Watch>)db.Watches.Where(w => w.UserName == userName && w.Date <= end && w.Date >= start);
            }
        }
    }
}
