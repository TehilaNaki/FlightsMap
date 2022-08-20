using BO;
using BO.Flights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    interface IDL
    {
        void AddUser(User u);
        void UpdatePassword(User u, string newPassword);
        bool ExistUser(User u);
        List<Watch> GetUserWatches(string userName, DateTime start, DateTime end);
        string GetNextWeekHolidies();
       // List<Flight> GetAllFlights();

        Dictionary<string, List<FlightInfoPartial>> GetCurrentFlights();
        FlightDetail GetFlightData(string key);

        Dictionary<string, string> GetCurrentWeather(string lon, string lat);
        Dictionary<string, string> GetLonLatOrigin(FlightDetail flight);
        Dictionary<string, string> GetLonLatDestination(FlightDetail flight);

    }
}
