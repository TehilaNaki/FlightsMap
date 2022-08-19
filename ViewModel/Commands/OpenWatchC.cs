using BO;
using FlightsMap.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightsMap.ViewModel.Commands
{
    public class OpenWatchC : ICommand
    {
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
            User u = (User)parameter;
            WatchesWin w = new WatchesWin();
            w.DataContext = new WatchWinVM(u,w.calender);
            w.Show();
        }
    }
}
