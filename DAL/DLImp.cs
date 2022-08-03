using DAL.DB;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DLImp : IDL
    {
        public void AddUser(User u)
        {
            using (var ctx = new FlightContext())
            {
                ctx.Users.Add(u);
                ctx.SaveChanges();
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

        public bool ExistUser(User u)
        {
            using (var ctx = new FlightContext())
            {
                if (ctx.Users.Find(u.UserId) != null)
                    return true;
                return false;
            }
        }

      
    }
}
