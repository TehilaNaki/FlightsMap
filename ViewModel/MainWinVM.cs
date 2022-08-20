using BL;
using BO;
using BO.Flights;
using FlightsMap.ViewModel.Commands;
using FlightsMap.Windows;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace FlightsMap.ViewModel
{
    public class MainWinVM: INotifyPropertyChanged
    {

       
        public MainWinVM()
        {
            ClockSign(3);
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

        private ObservableCollection<Pushpin> origin = new ObservableCollection<Pushpin>();
        public ObservableCollection<Pushpin> Origin
        {
            get
            {
                return origin;
            }
            set
            {
                push = value;
                OnPropertyChanged("Origin");
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
            ControlTemplate template = (ControlTemplate)MW.FindResource("pushpin_customIn");
            Pushpin push = new Pushpin
            {
                Location = new Location() { Latitude = f.Lat, Longitude = f.Long },
                Template = template,
                ToolTip = f.FlightCode,
            };
           push.MouseDown += new MouseButtonEventHandler((sender, e) => Push_MouseEnterIn(sender, e, f));
           Push.Add(push);
        }
        private void AddPushPineOut(FlightInfoPartial f)
        {
            ControlTemplate template = (ControlTemplate)MW.FindResource("pushpin_customOut");
            Pushpin push = new Pushpin
            {
                Location = new Location() { Latitude = f.Lat, Longitude = f.Long },
                Template = template,
                ToolTip = f.FlightCode,

            };
            push.MouseDown += new MouseButtonEventHandler((sender, e) => Push_MouseEnterOut(sender, e, f)); 
            Push.Add(push);
        }
        private void Refresh()
        {
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
        private void Push_MouseEnterOut(object sender, MouseButtonEventArgs e, FlightInfoPartial flight)
        {
            bl.AddWatch(new Watch { Date = DateTime.Now, Destination = flight.Destination, FlightNumber = flight.FlightCode, Origin = flight.Source, UserName = MyUser.UserId });
            var p = (Pushpin)sender;
            ControlTemplate template = (ControlTemplate)MW.FindResource("land");
            p.Template = template;
            UpdateFlight(flight);
        }
        private void Push_MouseEnterIn(object sender, MouseButtonEventArgs e, FlightInfoPartial flight)
        {
            bl.AddWatch(new Watch { Date = DateTime.Now, Destination = flight.Destination, FlightNumber = flight.FlightCode, Origin = flight.Source, UserName = MyUser.UserId });
            var p = (Pushpin)sender;
            ControlTemplate template = (ControlTemplate)MW.FindResource("takeoff");
            p.Template = template;
            UpdateFlight(flight);

            // open details window
            FlightDetailsWinVM fdvm = new FlightDetailsWinVM(flight);
            //fdvm.FlightPartial = flight;
            WinFlightDetails wfd = new WinFlightDetails();
            fdvm.WFD = wfd;
            fdvm.WFD.DataContext = fdvm;
            fdvm.WFD.Show();
        }
        private void UpdateFlight(FlightInfoPartial selected)
        {
            var Flight = bl.GetFlightDetail(selected.SourceId);

            // Update map
            if (Flight != null)
            {
                var OrderedPlaces = (from f in Flight.trail
                                     orderby f.ts
                                     select f).ToList<Trail>();

                addNewPolyLine(OrderedPlaces);

                Trail CurrentPlace = null;
                ControlTemplate template = (ControlTemplate)MW.FindResource("location");
                Pushpin PinCurrent = new Pushpin { ToolTip = selected.FlightCode,Template=template };
                Pushpin PinOrigin = new Pushpin { ToolTip = Flight.airport.origin.name, Template = template };

                PositionOrigin origin = new PositionOrigin { X = 0.4, Y = 0.4 };
                MapLayer.SetPositionOrigin(PinCurrent, origin);


                CurrentPlace = OrderedPlaces.Last<Trail>();
                var PlaneLocation = new Location { Latitude = CurrentPlace.lat, Longitude = CurrentPlace.lng };
                PinCurrent.Location = PlaneLocation;


                CurrentPlace = OrderedPlaces.First<Trail>();
                PlaneLocation = new Location { Latitude = CurrentPlace.lat, Longitude = CurrentPlace.lng };
                PinOrigin.Location = PlaneLocation;
                Origin.Clear();
                Origin.Add(PinOrigin);
            
            }
        }
        void addNewPolyLine(List<Trail> Route)
        {
            MapPolyline polyline = new MapPolyline();
           polyline.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Purple);
            polyline.StrokeThickness = 3;
            polyline.StrokeDashOffset = 2;
            polyline.StrokeDashArray = new System.Windows.Media.DoubleCollection() { 4, 4 };

            polyline.Locations = new LocationCollection();
            foreach (var item in Route)
            {
                polyline.Locations.Add(new Location(item.lat, item.lng));
            }
            MW.myMap.Children.RemoveRange(2, MW.myMap.Children.Count - 2);
            MW.myMap.Children.Add(polyline);
        }

    }
}
