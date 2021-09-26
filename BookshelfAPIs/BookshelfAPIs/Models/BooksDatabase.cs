using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Globalization;
using System.Data.SqlClient;
using System.Configuration;

namespace BookshelfAPIs.Models
{
    public class BooksDatabase : IBooksDatabase
    {

        private string connectionString = ConfigurationManager.ConnectionStrings["BookDB"].ConnectionString;
        public List<Book> Books = new List<Book>();
        private static BooksDatabase Instance = null;
        public static BooksDatabase instantiateDB()
        {
            if (Instance == null)
            {
                Instance = new BooksDatabase();
            }
            return Instance;
        }
        private BooksDatabase()
        {
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM books_collection";
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                Books = ConvertToBookList(dr);
            }
        }

        private List<Book> ConvertToBookList(SqlDataReader reader)
        {
            var rows = new List<Book>();
            while (reader.Read())
            {
                // rows.Add(columns.ToDictionary(column => column, column => reader[column]));
                rows.Add(new Book()
                {
                    Author = reader["Author"].ToString(),
                    Title = reader["Title"].ToString(),
                    Category = reader["Category"].ToString(),
                    ISBN = reader["ISBN"].ToString(),
                    Image = reader["Image"].ToString(),
                    Rating = double.Parse(reader["Rating"].ToString()),
                    Format = reader["Format"].ToString(),
                    Price = double.Parse(reader["Price"].ToString()),
                    OldPrice = double.Parse(reader["OldPrice"].ToString())
                });
            }
            return rows;
        }

        public List<Book> GetData()
        {
            return Books;
        }

        public Book GetData(string isbn)
        {
            return Books.FirstOrDefault(book => book.ISBN == isbn);
        }

        public string PostData(Book book)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = String.Format(
                    "insert into books_collection (Author, Title, Category, ISBN, Image, Rating, Format, Price, OldPrice) values ('{0}', '{1}', '{2}', '{3}', '{4}')",
                    book.Author, book.Title, book.Category, book.ISBN, book.Image, book.Rating, book.Format, book.Price, book.OldPrice
                    );
                con.Open();
                cmd.ExecuteNonQuery();
            }
            Books.Add(book);
            return String.Format(
                "Successfully added book! Author: {0} Title: {1} Category: {2} ISBN: {3} Date: {4}", 
                book.Author, book.Title, book.Category, book.ISBN, book.Image, book.Rating, book.Format, book.Price, book.OldPrice 
                );
        }
        public string DeleteData(Book Querybook)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = String.Format("DELETE FROM books_collection WHERE ISBN='{0}'", Querybook.ISBN);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            Books.Remove(Books.FirstOrDefault(book => book.ISBN == Querybook.ISBN));
            return String.Format("Deleted book where ISBN == {0}", Querybook.ISBN);
        }

        public List<Book> GetDataByCategory(string category)
        {
            throw new NotImplementedException();
        }
    }

}