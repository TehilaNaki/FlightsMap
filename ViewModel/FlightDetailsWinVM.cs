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

        public FlightInfoPartial FlightPartial { get; set; }
        public FlightDetail Flight { get; set; }

        public FlightDetailsWinVM(FlightInfoPartial fip)
        {
            FlightPartial = fip;
            Flight = bl.GetFlightDetail(FlightPartial.SourceId);
        }

        public string FlightNumber
        {
            get
            {               
                var flightnum = Flight.identification.number.Default;
                return flightnum.ToString();
            }
        }

        public string AirlineCompany
        {
            get
            {
                var airline = Flight.airline.name;
                return airline;
            }
        }

        public string Source
        {
            get
            {
                return FlightPartial.Source;
            }
        }

        public string Destination
        {
            get
            {
                return FlightPartial.Destination;
            }
        }

        public string SourceName
        {
            get
            {
                return Flight.airport.origin.name;
            }
        }

        public string DestinationName
        {
            get
            {
                return Flight.airport.destination.name;
            }
        }
        public String SSource
        {
            get
            {
                return Flight.time.scheduled.departure.ToString();
            }
        }

        public String SDest
        {
            get
            {
                return Flight.time.scheduled.arrival.ToString();
            }
        }
        public String Act
        {
            get
            {
                if(Flight.time.real.departure!=null)
                return Flight.time.real.departure.ToString();
                return Flight.time.estimated.departure.ToString();
            }
        }

        public String Est
        {
            get
            {
                if(Flight.time.estimated.arrival!=null)
                return Flight.time.estimated.arrival.ToString();
                return Flight.time.scheduled.arrival.ToString();


            }
        }

    }
}
