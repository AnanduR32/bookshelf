using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using BookshelfAPIs.Models;


namespace BookshelfAPIs.Models
{
    public class DataLoadLogic
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["BookDB"].ConnectionString;

        protected List<Book> LoadData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM books_collection";
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                return ConvertToBookList(dr);
            }
        }

        protected List<Book> ConvertToBookList(SqlDataReader reader)
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
    }
}