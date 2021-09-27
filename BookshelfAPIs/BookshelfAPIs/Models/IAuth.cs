using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshelfAPIs.Models
{
    interface IAuth
    {
        bool login(string username, string password);
        bool register(User user);
    }
}
