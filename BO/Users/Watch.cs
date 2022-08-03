using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   public class Watch
    {
        [Key, Column(Order = 0)]
        public string UserName { get; set; }
        [Key, Column(Order = 1)]
        public DateTime Date { get; set; }
        [Key, Column(Order = 2)]
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }

    }
}
