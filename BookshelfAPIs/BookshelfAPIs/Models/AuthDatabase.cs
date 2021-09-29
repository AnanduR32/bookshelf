using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Security.Cryptography;

namespace BookshelfAPIs.Models
{
    public class AuthDatabase : IAuth
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AuthDB"].ConnectionString;

        private static AuthDatabase Instance = null;
        public static AuthDatabase instantiateDB()
        {
            if (Instance == null)
            {
                Instance = new AuthDatabase();
            }
            return Instance;
        }
        private int count;
        private AuthDatabase() {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = String.Format("select count(*) from UserData");
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                count = dr.GetInt32(0);
            }
        }

        public User login(User user)
        {
            string pswdHashed = ComputeSha256Hash(user.Password);
            string pswdFromDB = String.Empty;
            User ResUser = new User();
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = String.Format("select Password from UserData where Email='{0}'", user.Email);
                con.Open();
                try
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    dr.Read();
                    pswdFromDB = dr.GetString(0);
                }
                catch(Exception e)
                {
                    throw new Exception("Failed to retrieve hashed password from database");
                }
                con.Close();

                if (pswdFromDB == pswdHashed)
                {
                    cmd.CommandText = String.Format("select * from UserData where Email='{0}'", user.Email);
                    con.Open();
                    try
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            ResUser.Name = dr["Name"].ToString();
                            ResUser.Email = dr["Email"].ToString();
                            ResUser.Mobile = Int32.Parse(dr["Mobile"].ToString());
                            ResUser.UserID = dr["UserID"].ToString();
                            ResUser.Password = dr["Password"].ToString();
                        }
                        return ResUser;
                    }
                    catch(Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    con.Close();
                    
                }
                else
                {
                    throw new Exception("Passwords do not match");
                }
            }

        }

        public bool register(User user)
        {
            user.UserID = "user_"+count;
            count++;
            int rows = 0;
            user.Password = ComputeSha256Hash(user.Password);
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = String.Format(
                    "insert into UserData (userID, Name, Email, Mobile, Password) values ('{0}', '{1}', '{2}', '{3}', '{4}')",
                    user.UserID, user.Name, user.Email, user.Mobile, user.Password
                    );
                con.Open();
                try
                {
                    rows = cmd.ExecuteNonQuery();
                }
                catch(Exception e)
                {
                    return false;
                }
                
            }
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return true;
            }
        }

        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}