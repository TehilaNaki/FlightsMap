using DAL.DB;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Json.Net;
using Newtonsoft.Json;

namespace DAL
{
    public class DLImp : IDL
    {
        public void AddUser(User u)
        {
            using (var db = new FlightContext())
            {
                db.Users.Add(u);
                db.SaveChanges();
            }
           
        }

        public void AddWatch(Watch w)
        {
            using (var db = new FlightContext())
            {
                db.Watches.Add(w);
                db.SaveChanges();
            }
        }

        public void UpdatePassword(User u, string newPassword)
        {
            using (var ctx = new FlightContext())
            {
                var user = ctx.Users.Find(u);
                if (user != null)
                {
                    ctx.Users.Remove(user);
                    user.Password = newPassword;
                    ctx.Users.Add(user);
                    ctx.SaveChanges();
                }

            }
        }

        public string GetNextWeekHolidies()
        {
            string start = DateTime.Today.ToString("yyyy-MM-dd").Replace('/','-');
            string end = DateTime.Today.AddDays(7).ToString("yyyy-MM-dd").Replace('/', '-');
            string URL = @"https://www.hebcal.com/hebcal?v=1&cfg=json&maj=on&min=on&mod=on&start="+ start + "&end=" + end;
            using(var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(URL);
                HolidayRoot holidayRoot = JsonConvert.DeserializeObject<HolidayRoot>(json);
                
                if (holidayRoot.items.Count > 0)
                {
                    holidayRoot.items.RemoveAll(i => i.subcat == "fast");
                    holidayRoot.items.Sort();
                    return holidayRoot.items.First().title;
                }
                return "";
            }
        }

        public bool ExistUser(User u)
        {
            using (var ctx = new FlightContext())
            {
                if (ctx.Users.Find(u.UserId) != null)
                    return true;
                return false;
            }
        }

        public List<Watch> GetUserWatches(string userName, DateTime start, DateTime end)
        {
            using (var db = new FlightContext())
            {
               return db.Watches.Where(w => w.UserName == userName && w.Date <= end && w.Date >= start).ToList();
            }
        }
    }
}
