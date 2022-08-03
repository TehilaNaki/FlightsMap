using DAL;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BL
{
    public class BLImp : IBL
    {
        DLImp dl = new DLImp();
        public void AddUser(User u)
        {
            dl.AddUser(u);
        }

        public void AddWatch(Watch w)
        {
            throw new NotImplementedException();
        }

        public bool ExistUser(User u)
        {
            return dl.ExistUser(u);
        }

        public List<Watch> GetAllWatches()
        {
            throw new NotImplementedException();
        }

        public List<Watch> GetUserWatches(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
