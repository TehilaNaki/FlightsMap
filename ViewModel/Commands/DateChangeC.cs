using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace FlightsMap.ViewModel.Commands
{
    public class DateChangeC : ICommand
    {
        WatchWinVM vm;
        public DateChangeC(WatchWinVM v)
        {
            vm = v;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            BLImp bl = new BLImp();
            var p = (CalenderWatchParameter)parameter;
            DateTime start = p.calender.SelectedDates.First();
            DateTime end = p.calender.SelectedDates.Last().AddHours(23.99999);
             vm.WatchList= bl.GetUserWatches(p.user.UserId, start, end);
        }
    }
}
