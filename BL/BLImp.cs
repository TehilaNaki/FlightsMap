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
        #endregion

        public List<Flight> getAllFlights()
        {
            throw new NotImplementedException();
        }

    }
}
