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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace FlightsMap.ViewModel
{
    public class WatchWinVM : INotifyPropertyChanged
    {
        public WatchWinVM(User u,Calendar c)
        {
            MyUser = u;        
            calendar = c;
            c.SelectedDatesChanged += DateChangedEvent;
            if(c.SelectedDates.Count==0)
            {
                c.SelectedDates.Add(DateTime.Today);
            }
            ClockSign(2);
        }
        
        public User MyUser { get; set; }
        public Calendar calendar { get; set; }
        BLImp bl = new BLImp();
        private ObservableCollection<Watch> watchList;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ObservableCollection<Watch> WatchList
        {
            get
            {
                return watchList;
            }
            set
            {
              
                watchList = value;
                OnPropertyChanged("WatchList");
            }
        }
        private void DateChangedEvent(object sender, RoutedEventArgs e)
        {
            DateTime start = calendar.SelectedDates.First();
            DateTime end = calendar.SelectedDates.Last().AddHours(23.99999);
            WatchList= new ObservableCollection<Watch>(bl.GetUserWatches(MyUser.UserId, start, end));
        }
        private void DateChanged()
        {
            if (calendar.SelectedDates.Count > 0)
            {
                DateTime start = calendar.SelectedDates.First();
                DateTime end = calendar.SelectedDates.Last().AddHours(23.99999);
                WatchList = new ObservableCollection<Watch>(bl.GetUserWatches(MyUser.UserId, start, end));
            }
            
        }
        private void ClockSign(int seconds)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += (s, e) => DateChanged();
            timer.Interval = new TimeSpan(0, 0, seconds);
            timer.Start();
        }
        public string Title
        {
            get
            {
                return "View History - " + MyUser.UserId;
            }
        }
    }
}
