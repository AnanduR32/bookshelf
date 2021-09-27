using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookshelfAPIs.Models;
using System.Web.Http.Cors;

namespace BookshelfAPIs.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CategoriesController : ApiController
    {
        CategoriesDatabase db = CategoriesDatabase.instantiateDB();

        [HttpGet]
        public List<string> GetData()
        {
            return db.GetCategories();
        }
        public List<Book> GetData(string category)
        {
            return db.GetDataByCategory(category);
        }
    }
}