﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Globalization;
using System.Data.SqlClient;
using System.Configuration;

namespace BookshelfAPIs.Models
{
    public class BooksDatabase : DataLoadLogic, IBooksDatabase
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
            Books = LoadData();
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
                    "insert into books_collection (Author, Title, Category, ISBN, Image, Rating, Format, Price, OldPrice) values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}')",
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

        public string PutData(Book book)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = String.Format(
                    "update books_collection set Author='{0}', Title='{1}', Category='{2}', ISBN='{3}', Image='{4}', Rating='{5}', Format='{6}', Price='{7}', OldPrice='{8}' where ISBN='{3}'",
                    book.Author, book.Title, book.Category, book.ISBN, book.Image, book.Rating, book.Format, book.Price, book.OldPrice
                    );
                con.Open();
                cmd.ExecuteNonQuery();
            }
            Books = LoadData();
            return String.Format(
                "Successfully updated book! Author: {0} Title: {1} Category: {2} ISBN: {3} Price: {8}",
                book.Author, book.Title, book.Category, book.ISBN, book.Image, book.Rating, book.Format, book.Price, book.OldPrice
                );
        }

        public string DeleteData(string isbn)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = String.Format("DELETE FROM books_collection WHERE ISBN='{0}'", isbn);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            Books.Remove(Books.FirstOrDefault(book => book.ISBN == isbn));
            return String.Format("Deleted book where ISBN == {0}", isbn);
        }
    }

}