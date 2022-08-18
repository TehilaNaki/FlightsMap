using BL;
using BO;
using BO.Flights;
using FlightsMap.ViewModel.Commands;
using Microsoft.Maps.MapControl.WPF;
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
   public class MainWinVM: INotifyPropertyChanged
    {

       
    public MainWinVM()
        {
            ClockSign(1);
            watchCmd = new OpenWatchC();
           
        }
        public ICommand watchCmd { get; set; }
        public User MyUser { get; set; }
        public MainWindow MW { get; set; }
         BLImp bl = new BLImp();
        
        private ObservableCollection<Pushpin> push=new ObservableCollection<Pushpin>();
        public ObservableCollection<Pushpin>Push { 
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
        private void AddPushPineIn(FlightInfoPartial f)
        {
            Pushpin p = new Pushpin();
            p.Location = new Location(33, 32);
            ControlTemplate template = (ControlTemplate)MW.FindResource("pushpin_customIn");
            Pushpin push = new Pushpin
            {
                Location = new Location() { Latitude = f.Lat, Longitude = f.Long },
                Template = template,
                ToolTip = f.FlightCode,
            };
          //  push.MouseDoubleClick += new MouseButtonEventHandler((sender, e) => Push_MouseEnter(sender, e, f)); ;
            Push.Add(push);
        }
        private void AddPushPineOut(FlightInfoPartial f)
        {
            Pushpin p = new Pushpin();
            p.Location = new Location(33, 32);
            ControlTemplate template = (ControlTemplate)MW.FindResource("pushpin_customOut");
            Pushpin push = new Pushpin
            {
                Location = new Location() { Latitude = f.Lat, Longitude = f.Long },
                Template = template,
                ToolTip = f.FlightCode,
            };
            push.MouseDoubleClick += new MouseButtonEventHandler((sender, e) => Push_MouseEnter(sender, e, f)); ;
            Push.Add(push);
        }
        private void Refresh()
        {
            //ControlTemplate template = (ControlTemplate)MW.FindResource("pushpin_custom");            
            // Push.Add(new Pushpin { Location = new Location(33, 32), ToolTip = "sss" ,Template=template});
            //Push.Add(new Pushpin { Location = new Location(34, 32), ToolTip = "sg", Template = template });
            var f = bl.GetCurrentFlights();
            Push.Clear();
            foreach (var flight in f["Incoming"])
            {
                AddPushPineIn(flight);
            }
            foreach (var flight in f["Outgoing"])
            {
                AddPushPineOut(flight);
            }
        }
        private void ClockSign(int seconds)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += (s, e) => Refresh();
            timer.Interval = new TimeSpan(0, 0, seconds);
            timer.Start();
        }
        private void Push_MouseEnter(object sender, MouseButtonEventArgs e, FlightInfoPartial flight)
        {
              bl.AddWatch(new Watch { Date = DateTime.Now, Destination = flight.Destination, FlightNumber = flight.FlightCode, Origin = flight.Source, UserName = MyUser.UserId });
        }

    }
}
