using BL;
using BO;
using FlightsMap.Windows;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;




namespace FlightsMap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User MyUser { get; set; }
        BLImp bl=new BLImp();
        List<Pushpin>Push=new List<Pushpin>();
        public MainWindow()
        {
            InitializeComponent();
            MyUser = new User { UserId = "t", Password = "t", Email = "crrr" };
            //  Refresh();
            AddPushPine(new Flight { Destination = "cf", FlightNumber = "H233", Latitude = 33, Longtitude = 32, Origin = "TLV" });
            setTitle();           
           // ClockSign(10);
        }

        private void setTitle()
        {
            string holiday=bl.GetNextWeekHolidies().ToUpper();                
            holidayTitle.Text = holiday != ""? "- Now is a Holiday Week: " + holiday: "";           
        }

        private void ClockSign(int seconds)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += (s, e) => Refresh();
            timer.Interval = new TimeSpan(0, 0, seconds);
            timer.Start();
        }
        private void AddPushPine(Flight f)
        {
            Pushpin p = new Pushpin();
            p.Location = new Location(33, 32);
            ControlTemplate template = (ControlTemplate)this.FindResource("pushpin_custom");
            Pushpin push = new Pushpin
            {
                Location = new Location() { Latitude = f.Longtitude, Longitude = f.Latitude },
                Template = template,
                ToolTip = f.FlightNumber,
            };
            push.MouseDoubleClick += new MouseButtonEventHandler((sender, e) => Push_MouseEnter(sender, e, f)); ;
            Push.Add(push);
        }

        private void Push_MouseEnter(object sender, MouseButtonEventArgs e, Flight flight)
        {
            bl.AddWatch(new Watch { Date = DateTime.Now, Destination = flight.Destination, FlightNumber = flight.FlightNumber, Origin = flight.Origin, UserName = MyUser.UserId });
        }

        private void WatchClick(object sender, RoutedEventArgs e)
        {
            WatchesWin w = new WatchesWin(MyUser);
            w.Show();
        }
        private void Refresh()
        {
          List<Flight> l=  bl.getAllFlights();
            myMap.Children.Clear();
            foreach (var flight in l)
            {
                AddPushPine(flight);
            }
        }
        public ObservableCollection<MapPoints> OcMapPoints { get; set; }

        private void LoadPoints()
        {
            OcMapPoints = new ObservableCollection<MapPoints>();

            var rand = new Random();

            for (int i = 0; i < 100; i++)
            {
                OcMapPoints.Add(new MapPoints()
                {
                    Latitude = rand.NextDouble() * 180 - 90,
                    Longitude = rand.NextDouble() * 360 - 180,
                });
            }
        }
}
