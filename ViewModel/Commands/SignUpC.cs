using BL;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FlightsMap.ViewModel.Commands
{
  
    public class SignUpC : ICommand
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
            var p = (User)parameter;
            if (p.Password.Length == 0 ||p.UserId == "")
            {
                MessageBox.Show("Please enter user name and password");
            }
            else
            {
                new BLImp().AddUser(p);
                MessageBox.Show("User: " + p.UserId + " added succsesfully!");
                MainWinVM mvm = new MainWinVM();
                mvm.MyUser = p;
                mvm.MW = new MainWindow();
                mvm.MW.DataContext = mvm;
                
                mvm.MW.Show();
            }
        }
    }
}
