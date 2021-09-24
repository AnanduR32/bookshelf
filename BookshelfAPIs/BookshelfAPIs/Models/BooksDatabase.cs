using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Globalization;
using System.Data.SqlClient;

namespace BookshelfAPIs.Models
{
    public class BooksDatabase
    {

        private string connectionString = @"Data Source=localhost\SQLEXPRESS01; database=BookshelfDB; Trusted_Connection=True";
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

        public void PostData(Book book)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = String.Format("insert into books_collection (Author, Name, ISBN, Date) values ('{0}', '{1}', '{2}', '{3}')",book.Author, book.Name, book.ISBN, book.Date);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            Books.Add(book);
        }
        public void DeleteData(Book Querybook)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = String.Format("DELETE FROM books_collection WHERE ISBN='{0}'", Querybook.ISBN);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            Books.Remove(Books.FirstOrDefault(book => book.ISBN == Querybook.ISBN));
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
                    Name = reader["Name"].ToString(),
                    ISBN = reader["ISBN"].ToString(),
                    Date = reader["Date"].ToString(),
                });
            }
            return rows;
        }
    }
    public class Book
    {
        public string ISBN { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Date { get; set; }
    }
}