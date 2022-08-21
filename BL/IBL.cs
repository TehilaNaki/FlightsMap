using BO;
using BO.Flights;
using System;
using System.Collections.Generic;

namespace BL
{
    interface IBL
    {
        void AddUser(User u);
        bool ExistUser(User u);
        void AddWatch(Watch w);
        List<Watch> GetAllWatches();
        List<Watch> GetUserWatches(string userName, DateTime start, DateTime end);
        string GetNextWeekHolidies();

        Dictionary<string, List<FlightInfoPartial>> GetCurrentFlights();
        FlightDetail GetFlightDetail(string partialFlightID);

        Dictionary<string, Dictionary<string, string>> GetWeather(FlightDetail flight, FlightInfoPartial fip);
   
        double GetDistance(FlightDetail flight);
        int GetProp(FlightDetail flight, FlightInfoPartial fip);
        double GetRemainingDistance(FlightDetail flight, FlightInfoPartial fip);

        string GetStringRemainingTime(FlightDetail flight);
    }
}
