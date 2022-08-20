using DAL;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BO.Flights;

namespace BL
{
    public class BLImp : IBL
    {
        DLImp dl = new DLImp();
        public void AddUser(User u)
        {
            dl.AddUser(u);
        }

        public void AddWatch(Watch w)
        {
            dl.AddWatch(w);
        }

        public bool ExistUser(User u)
        {
            return dl.ExistUser(u);
        }

        public List<Watch> GetAllWatches()
        {
            throw new NotImplementedException();
        }

        

        public string GetNextWeekHolidies()
        {
            return dl.GetNextWeekHolidies();
        }

        public List<Watch> GetUserWatches(string userName,DateTime start,DateTime end)
        {

            return dl.GetUserWatches(userName, start, end);
              //  return new List<Watch> { new Watch { Destination = "Fdc", Origin = "rcd", Date = new DateTime(2022, 10, 21) } };
        }

        #region flights
        public Dictionary<string, List<FlightInfoPartial>> GetCurrentFlights()
        {
           return dl.GetCurrentFlights();
        }

        public FlightDetail GetFlightDetail(string partialFlightID)
        {
            return dl.GetFlightData(partialFlightID);
        }

        public Dictionary<string, Dictionary<string, string>> GetWeather(FlightDetail flight ,FlightInfoPartial fip)
        {
           
            Dictionary<string, Dictionary<string, string>> result = new Dictionary<string, Dictionary<string, string>>();

            Dictionary<string, string> currentW = dl.GetCurrentWeather(fip.Long.ToString(), fip.Lat.ToString());
            Dictionary<string, string> originW = dl.GetCurrentWeather(dl.GetLonLatOrigin(flight)["lon"], dl.GetLonLatOrigin(flight)["lat"]);
            Dictionary<string, string> destinationW = dl.GetCurrentWeather(dl.GetLonLatDestination(flight)["lon"], dl.GetLonLatDestination(flight)["lat"]);

            result.Add("current", currentW);
            result.Add("origin", originW);
            result.Add("destination", destinationW);

            return result;
        }
        #endregion



    }
}
