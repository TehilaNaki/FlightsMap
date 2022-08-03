using BL;
using FlightsMap.PO;
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

namespace FlightsMap
{
    /// <summary>
    /// Interaction logic for enterWin.xaml
    /// </summary>
    public partial class enterWin : Window
    {
        UserPO myUser = new UserPO { UserId = "tehila", Password = "11", Email = "11@" };
        BLImp bl = new BLImp();

        public enterWin()
        {
            InitializeComponent();
            this.DataContext = myUser;
        }

        private void SignUpClick(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Text.Length == 0 || username1.Text == "")
            {
                MessageBox.Show("Please enter user name and password");
            }
            else
            {
                bl.AddUser(new BO.User { UserId = myUser.UserId, Password = myUser.Password, Email = myUser.Email });
                MessageBox.Show("Manager user:" + myUser.UserId + " added succsesfully!");
                MainWindow main = new MainWindow(myUser);
                main.Show();
                passwordBox.Text = "";
                username1.Text = "";
                Close();
            }
                
        }
        private void EnterClick(object sender, RoutedEventArgs e)
        {
            if (password.Text.Length == 0 || username.Text == "")
                MessageBox.Show("Please enter User name and Password");
            else
            {

                if (bl.ExistUser(new BO.User { UserId = myUser.UserId, Password = myUser.Password, Email = myUser.Email }))
                {
                    MainWindow main = new MainWindow(myUser);
                    main.Show();
                    passwordBox.Text = "";
                    username1.Text = "";
                    Close();
                }
                else MessageBox.Show("Incorrect Username or Password");

            }
        }
    }
}
