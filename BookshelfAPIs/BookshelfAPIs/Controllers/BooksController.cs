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
        public List<Book> GetBooks()
        {
            return db.GetData();
        }
        public Book GetBooks(string isbn)
        {
            return db.GetData(isbn);
        }
        
        [HttpPost]
        public string PostBook(Book book)
        {
            return db.PostData(book);
        }
        [HttpDelete]
        public string DeleteBook(Book book)
        {
            return db.DeleteData(book);
        }
    }
}