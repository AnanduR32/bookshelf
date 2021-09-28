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
    public class BooksController : ApiController
    {
        BooksDatabase db = BooksDatabase.instantiateDB();

        [HttpGet]
        public HttpResponseMessage GetBooks()
        {
            try
            {
                List<Book> data = db.GetData();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
        }
        public HttpResponseMessage GetBooks(string isbn)
        {
            try
            {
                Book data = db.GetData(isbn);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [HttpPut]
        public string PutData(Book book)
        {
            return db.PutData(book);
        }


        [HttpPost]
        public string PostBook(Book book)
        {
            return db.PostData(book);
        }
        [HttpDelete]
        public string DeleteBook(string isbn)
        {
            return db.DeleteData(isbn);
        }
    }
}