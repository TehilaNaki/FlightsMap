using BL;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FlightsMap.Windows
{
    /// <summary>
    /// Interaction logic for WatchesWin.xaml
    /// </summary>
    public partial class WatchesWin : Window
    {
        
        public WatchesWin()
        {
            InitializeComponent();        
        }

        private void DateChangedEvent(object sender, RoutedEventArgs e)
        {
            //DateTime start = calender.SelectedDates.First();
            //DateTime end = calender.SelectedDates.Last().AddHours(23.99999);
            //listView.ItemsSource = bl.GetUserWatches(myUser.UserId,start,end);
        }
    }
}
