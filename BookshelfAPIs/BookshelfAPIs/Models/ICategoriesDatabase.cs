using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshelfAPIs.Models
{
    interface ICategoriesDatabase
    {
        List<string> GetCategories();
        List<Book> GetDataByCategory(string category);
    }
}
