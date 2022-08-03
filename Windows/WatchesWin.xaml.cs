using BL;
using BO;
using FlightsMap.PO;
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
        UserPO myUser;
        BLImp bl = new BLImp();
        ObservableCollection<Watch> watchList;
        public WatchesWin(UserPO u)
        {
            InitializeComponent();
            myUser = u;
            watchList= new ObservableCollection<Watch>(bl.GetUserWatches(myUser.UserId));
        }
    }
}
