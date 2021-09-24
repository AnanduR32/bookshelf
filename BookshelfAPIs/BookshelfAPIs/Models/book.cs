using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookshelfAPIs.Models
{
    public class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Date { get; set; }
    }
}