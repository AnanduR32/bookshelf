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
        public HttpResponseMessage GetData()
        {
            try
            {
                List<string> data = db.GetCategories();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict);
            }
        }
        public HttpResponseMessage GetData(string category)
        {
            try
            {
                List<Book> data = db.GetDataByCategory(category);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict);
            }
        }
    }
}