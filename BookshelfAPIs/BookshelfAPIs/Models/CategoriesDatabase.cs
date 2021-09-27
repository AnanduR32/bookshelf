using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using BookshelfAPIs.Models;

namespace BookshelfAPIs.Models
{
    public class CategoriesDatabase : DataLoadLogic, ICategoriesDatabase
    {
        public List<Book> Books = new List<Book>();
        private static CategoriesDatabase Instance = null;
        public static CategoriesDatabase instantiateDB()
        {
            if (Instance == null)
            {
                Instance = new CategoriesDatabase();
            }
            return Instance;
        }
        private CategoriesDatabase()
        {
            Books = LoadData();
        }

        public List<string> GetCategories()
        {
            return Books.Select(x => x.Category).Distinct().ToList();

        }

        public List<Book> GetDataByCategory(string category)
        {
            return Books.FindAll(x => x.Category == category);
        }
    }
}