using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   public class Leyning
    {
        public string _1 { get; set; }
        public string _2 { get; set; }
        public string _3 { get; set; }
        public string torah { get; set; }
        public string haftarah { get; set; }
        public string _4 { get; set; }
        public string _5 { get; set; }
        public string maftir { get; set; }
    }
    public class Item:IComparable
    {
        public string title { get; set; }
        public string date { get; set; }
        public string hdate { get; set; }
        public string category { get; set; }
        public string subcat { get; set; }
        public string hebrew { get; set; }
        public string link { get; set; }
        public string memo { get; set; }
        public Leyning l { get; set; }
        public bool? yomtov { get; set; }

        public int CompareTo(object obj)
        {
            Item i2 = (Item)obj;
            if (i2.subcat == "minor" && subcat == "major")
                return -1;
            if (subcat == "minor" && i2.subcat == "major")
                return 1;
            return 0;
        }
    }
    
    public class HolidayRoot
    {
        public string title { get; set; }
        public DateTime date { get; set; }
        public Loc location { get; set; }
        public Range range { get; set; }
        public List<Item> items { get; set; }
    }
    public class Loc
    {
        public string geo { get; set; }
    }

    public class Range
    {
        public string start { get; set; }
        public string end { get; set; }
    }
}
