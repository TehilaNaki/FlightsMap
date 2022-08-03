using FlightsMap.PO;
using FlightsMap.Windows;
using System.Windows;

namespace FlightsMap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserPO MyUser;
        public MainWindow(UserPO u)
        {
            InitializeComponent();
            MyUser = u;
            myMap.Focus();

        }


        private void WatchClick(object sender, RoutedEventArgs e)
        {
            WatchesWin w = new WatchesWin(MyUser);
            w.Show();
        }
    }
}
