﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshelfAPIs.Models
{
    interface IBooksDatabase
    {
        List<Book> GetData();
        Book GetData(string isbn);
        string PostData(Book book);
        string DeleteData(Book book);

    }
}
