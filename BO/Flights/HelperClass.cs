using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.Flights
{
    public class HelperClass
    {
        public HelperClass() { }
        public DateTime GetDateTimeFromEpoch(double EpochTimeStamp) {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            start = start.AddSeconds(EpochTimeStamp);
            return start;
        }
    }
}
