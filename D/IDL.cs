using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    interface IDL
    {
        void AddUser(User u);
        void UpdatePassword(User u, string newPassword);
        bool ExistUser(User u);

    }
}
