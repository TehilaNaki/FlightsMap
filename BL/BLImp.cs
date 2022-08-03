using DAL;
using BO;
using System;

namespace BL
{
    public class BLImp : IBL
    {
        DLImp dl = new DLImp();
        public void AddUser(User u)
        {
            dl.AddUser(u);
        }

        public bool ExistUser(User u)
        {
            return dl.ExistUser(u);
        }
    }
}
