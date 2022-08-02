using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class FlightContext:DbContext
    {
        public FlightContext(): base("FlightsMapDB"){}

    }
}
