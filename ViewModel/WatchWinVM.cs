using BL;
using BO;
using FlightsMap.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightsMap.ViewModel
{
    public class WatchWinVM : INotifyPropertyChanged
    {
        public WatchWinVM(User u)
        {
            MyUser = u;
            dateChange = new DateChangeC(this);
        }
        
        public User MyUser { get; set; }
        BLImp bl = new BLImp();
        public List<Watch> WatchList
        {
            get
            {
                return bl.GetUserWatches(MyUser.UserId, DateTime.Today, DateTime.Now);
            }
            set
            {
                WatchList=value;
            }
        }
        public ICommand dateChange;
        public event PropertyChangedEventHandler PropertyChanged;
        //public CalenderWatchParameter CalenderP { get { return new CalenderWatchParameter{calender= } set; }
    }
}
