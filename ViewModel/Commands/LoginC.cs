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
   public class LoginC:ICommand
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

            if (p.Password.Length == 0 || p.UserId == "")
                MessageBox.Show("Please enter User name and Password");
            else
            {
                if (new BLImp().ExistUser(new BO.User { UserId = p.UserId, Password = p.Password, Email = p.Email }))
                {
                                     
                    MainWinVM mvm = new MainWinVM();
                    mvm.MyUser = p;
                    MainWindow main = new MainWindow();
                    mvm.MW = new MainWindow();
                    mvm.MW.DataContext = mvm;
                    mvm.MW.Show();
                }
                else MessageBox.Show("Incorrect Username or Password");

            }

        }
    }
}
