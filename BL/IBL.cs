using BO;
using System;
using System.Collections.Generic;

namespace BL
{
    interface IBL
    {
        void AddUser(User u);
        bool ExistUser(User u);
        void AddWatch(Watch w);
        List<Watch> GetAllWatches();
        List<Watch> GetUserWatches(string userName, DateTime start, DateTime end);
        string GetNextWeekHolidies();
    }
}
