using BL;
using BO;
using FlightsMap.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightsMap.ViewModel
{
   public class EnterWinVM
    {
        public EnterWinVM()
        {
            SignC=new SignUpC();
            LogC = new LoginC();
            MyUserL = new User();
            MyUserS=new User();
        }
    
        BLImp bl = new BLImp();
        public ICommand SignC { get; set; }
        public ICommand LogC { get; set; }
        public User MyUserL { get; set; }
        public User MyUserS { get; set; }
    }
}
