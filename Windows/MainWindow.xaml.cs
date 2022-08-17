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
       
        public MainWindow()
        {
            InitializeComponent();
           // AddPushPine(new Flight { Destination = "cf", FlightNumber = "H233", Latitude = 33, Longtitude = 32, Origin = "TLV" });
        }

        //private void ClockSign(int seconds)
        //{
        //    DispatcherTimer timer = new DispatcherTimer();
        //    timer.Tick += (s, e) => Refresh();
        //    timer.Interval = new TimeSpan(0, 0, seconds);
        //    timer.Start();
        //}
       

        private void Push_MouseEnter(object sender, MouseButtonEventArgs e, Flight flight)
        {
          //  bl.AddWatch(new Watch { Date = DateTime.Now, Destination = flight.Destination, FlightNumber = flight.FlightNumber, Origin = flight.Origin, UserName = MyUser.UserId });
        }

        private void WatchClick(object sender, RoutedEventArgs e)
        {
            //WatchesWin w = new WatchesWin(MyUser);
            //w.Show();
        }

        //  public ObservableCollection<MapPoints> OcMapPoints { get; set; }

        //private void LoadPoints()
        //{
        //    OcMapPoints = new ObservableCollection<MapPoints>();

        //    var rand = new Random();

        //    for (int i = 0; i < 100; i++)
        //    {
        //        OcMapPoints.Add(new MapPoints()
        //        {
        //            Latitude = rand.NextDouble() * 180 - 90,
        //            Longitude = rand.NextDouble() * 360 - 180,
        //        });
        //    }
        //}
    }
}
