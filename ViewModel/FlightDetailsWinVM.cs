using BL;
using BO;
using BO.Flights;
using FlightsMap.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsMap.ViewModel
{
    internal class FlightDetailsWinVM 
    {
        public WinFlightDetails WFD { get; set; }
        BLImp bl = new BLImp();
        public HelperClass helper; 

        public FlightInfoPartial FlightPartial { get; set; }
        public FlightDetail Flight { get; set; }

        public FlightDetailsWinVM(FlightInfoPartial fip)
        {
            FlightPartial = fip;
            Flight = bl.GetFlightDetail(FlightPartial.SourceId);
            helper = new HelperClass();
        }
        public string FlightNumber
        {
            get
            {
                return bl.GetFlightNumber(Flight);
            }
        }
        public string AirlineCompany
        {
            get
            {
                return bl.GetAirlineCompany(Flight);
            }
        }
        public string Source
        {
            get
            {
                return bl.GetOrigin(FlightPartial);
            }
        }
        public string Destination
        {
            get
            {
                return bl.GetDestination(FlightPartial);
            }
        }
        public string SourceName
        {
            get
            {
                return bl.GetOriginName(Flight);
            }
        }
        public string DestinationName
        {
            get
            {
                return bl.GetDestName(Flight);
            }
        }
        public string SDest
        {
            get
            {
                return bl.GetScheDest(Flight);


            }
        }
        public string SSource
        {
            get
            {
                return bl.GetSSource(Flight);

            }
        }
        public string Act
        {
            get
            {
                return bl.GetActual(Flight);

            }
        }
        public string Est
        {
            get
            {
                return bl.GetEstimated(Flight);

            }
        }
        public string StatusAirplane
        {
            get
            {
                return bl.GetStatusAirplane(Flight);

            }
        }
        public string FlightStatus
        {
            get
            {
                return bl.GetFlightStatus(Flight);

            }
        }

        public string Stimezone
        {
            get
            {
                return bl.GetSTimezone(Flight);



            }
        }   
        public string Dtimezone
        {
            get
            {
                return bl.GetDTimezone(Flight);


            }
        }
        public int PBValue
        {
            get
            {
                return bl.GetProp(Flight, FlightPartial);
            }
        }
        public string PBStatus
        {
            get
            {
                return bl.GetRemainingDistance(Flight,FlightPartial).ToString("F1")+" km in "+bl.GetStringRemainingTime(Flight)+" Left.";
            }
        }

        public string WeatherOrigin
        {
            get
            {
                var result = bl.GetWeather(Flight, FlightPartial);
                //This function returns a Dictionary of dictionaries.

                // there are 3 locations: current, origin and destination
                // for each location there are : temperature, main and shortDesc

                // For example, to have the temperature of the destination 
                // it is :
                return result["origin"]["shortDesc"].ToUpper() + " "+result["origin"]["temperature"] + " °C";
            }
        }
        public string WeatherDest
        {
            get
            {
                var result = bl.GetWeather(Flight, FlightPartial);
                return result["destination"]["shortDesc"].ToUpper() + " "+result["destination"]["temperature"] + " °C";
            }
        }
        public string WeatherCurrent
        {
            get
            {
                var result = bl.GetWeather(Flight, FlightPartial);
                return result["current"]["shortDesc"].ToUpper() + " "+result["current"]["temperature"]+ " °C";
            }
        }

    }
}
