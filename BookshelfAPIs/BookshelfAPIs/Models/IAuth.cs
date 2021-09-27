using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshelfAPIs.Models
{
    interface IAuth
    {
        bool login(User user);
        bool register(User user);
    }
}
