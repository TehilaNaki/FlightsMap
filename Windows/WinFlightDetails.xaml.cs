using BL;
using BO;
using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour WinFlightDetails.xaml
    /// </summary>
    public partial class WinFlightDetails : Window
    {
        BLImp bl = new BLImp();
        FlightDetail _curFlight;
        public WinFlightDetails(string flightId)
        {
            InitializeComponent();
            this._curFlight=bl.GetFlightDetail(flightId);

        }


    }
}
