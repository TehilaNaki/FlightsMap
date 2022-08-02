using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DO
{
    public class Aircraft//כלי טיס
    {
        public Model model { get; set; }
        public int countryId { get; set; }
        public string registration { get; set; }
        public string hex { get; set; }
        public float age { get; set; }
        public object msn { get; set; }
        public Images images { get; set; }
        public Identification identification { get; set; }
        public Airport airport { get; set; }
        public Time time { get; set; }
    }
}
