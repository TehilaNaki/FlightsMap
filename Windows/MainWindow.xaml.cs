using BL;
using BO;
using FlightsMap.PO;
using FlightsMap.Windows;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace FlightsMap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserPO MyUser { get; set; }
        BLImp bl=new BLImp();
        public MainWindow()
        {
            InitializeComponent();
           // MyUser = u;
            myMap.Focus();
            AddPushPines();
            setTitle();
           
           // ClockSign(10);
        }

        private void setTitle()
        {
            string holiday=bl.GetNextWeekHolidies();
            if (holiday != "")
            {
                holidayTitle.Content = "Flights Center - Now is a Holiday Week :" + holiday;
            }
            else holidayTitle.Content = "Flights Center";


        }

        private void ClockSign(int seconds)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += (s, e) => Refresh();
            timer.Interval = new TimeSpan(0, 0, seconds);
            timer.Start();
        }
        private void AddPushPines()
        {
            Pushpin p = new Pushpin();
            p.Location = new Location(33, 32);
            ControlTemplate template = (ControlTemplate)this.FindResource("pushpin_custom");
            Pushpin push = new Pushpin
            {
                Location = new Location() { Latitude = 33, Longitude = 33 },
                Template = template,
            };
            push.MouseDoubleClick += Push_MouseEnter;
            myMap.Children.Add(push);
        }
        private void Push_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            MessageBox.Show("rrrrrr");
        }

        private void WatchClick(object sender, RoutedEventArgs e)
        {
            WatchesWin w = new WatchesWin(MyUser);
            w.Show();
        }
        private void Refresh()
        {
            MessageBox.Show("rrrrrr");
        }
    }
}
