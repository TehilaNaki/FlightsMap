using BL;
using BO;
using FlightsMap.ViewModel.Commands;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace FlightsMap.ViewModel
{
   public class MainWinVM: INotifyPropertyChanged
    {

       
    public MainWinVM()
        {
            ClockSign(1);
            Push.Add(new Pushpin { Location = new Location(33, 32), ToolTip = "sss" });
            watchCmd = new OpenWatchC();
           
        }
        public ICommand watchCmd { get; set; }
        public User MyUser { get; set; }
        public MainWindow MW { get; set; }
         BLImp bl = new BLImp();
        
        private List<Pushpin> push=new List<Pushpin>();
        public List<Pushpin>Push { 
            get
            {
                return push;
            }
            set
            {
                push = value;
                OnPropertyChanged("Push");
            }
        }
        public string TitleText
        {
            get
            {
                
                string holiday = bl.GetNextWeekHolidies().ToUpper();
                return holiday != "" ? "- Now is a Holiday Week: " + holiday : "";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private void AddPushPine(Flight f)
        {
            Pushpin p = new Pushpin();
            p.Location = new Location(33, 32);
            ControlTemplate template = (ControlTemplate)MW.FindResource("pushpin_custom");
            Pushpin push = new Pushpin
            {
                Location = new Location() { Latitude = f.Longtitude, Longitude = f.Latitude },
                Template = template,
                ToolTip = f.FlightNumber,
            };
          //  push.MouseDoubleClick += new MouseButtonEventHandler((sender, e) => Push_MouseEnter(sender, e, f)); ;
            Push.Add(push);
        }
        private void Refresh()
        {
           // ControlTemplate template = (ControlTemplate)MW.FindResource("pushpin_custom");
            Push.Add(new Pushpin { Location = new Location(33, 32), ToolTip = "sss"});
           // Push.Add(new Pushpin { Location = new Location(34, 32), ToolTip = "sg", Template = template });
            //List<Flight> l = bl.getAllFlights();
            //Push.Clear();
            //foreach (var flight in l)
            //{
            //    AddPushPine(flight);
            //}
        }
        private void ClockSign(int seconds)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += (s, e) => Refresh();
            timer.Interval = new TimeSpan(0, 0, seconds);
            timer.Start();
        }
       
    }
}
