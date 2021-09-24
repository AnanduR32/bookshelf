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
            return db.Books;
        }
        public Book GetBooks(string isbn)
        {
            return db.Books.FirstOrDefault(book => book.ISBN == isbn);
        }
        
        [HttpPost]
        public string PostBook(Book book)
        {
            db.PostData(book);
            return String.Format("Successfully added book! Author: {0} Name: {1} ISBN: {2} Date: {3}", book.Author, book.Name, book.ISBN, book.Date);
        }
        [HttpDelete]
        public string DeleteBook(Book book)
        {
            db.DeleteData(book);
            return String.Format("Deleted book where ISBN == {0}", book.ISBN);
        }
    }
}