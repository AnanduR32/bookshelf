using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookshelfAPIs.Models
{
    public class Book
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Format { get; set; }
        public double Rating { get; set; }
        public double Price { get; set; }
        public double OldPrice { get; set; }
        public string ISBN { get; set; }
        public string Category { get; set; }


    }
}