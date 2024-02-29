using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.RecipeApp.Repository
{
    public class UserRepository
    {
        private static string _connectionString = ConnectionStringProvider.GetConnectionString();

        public string User;

        public bool CreateUser(string username,
                               string password,
                               string email,
                               string firstName,
                               string lastName,
                               string contentBio,
                               string imageSource,
                               string isAdmin,
                               string isBlocked)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO [dbo].[user] (username, password, email, first_name, last_name, content_bio, image_source, is_admin, is_blocked, created_at)
                             VALUES (@username, @password, @email, @firstName, @lastName, @contentBio, @imageSource, @isAdmin, @isBlocked, @created_at)";

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    cmd.Connection = con;
                    // Add parameter to command
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@lastName", lastName);
                    cmd.Parameters.AddWithValue("@contentBio", contentBio);
                    cmd.Parameters.AddWithValue("@imageSource", imageSource);
                    cmd.Parameters.AddWithValue("@isAdmin", isAdmin);
                    cmd.Parameters.AddWithValue("@isBlocked", isBlocked);
                    cmd.Parameters.AddWithValue("@created_at", DateTime.UtcNow);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }

        public List<string> GetAllUsers()
        {
            List<string> returnUser = new List<string>();

            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM [dbo].[user]";

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    cmd.Connection = con;

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string user = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}",
                                                          reader["ID"],
                                                          reader["username"],
                                                          reader["password"],
                                                          reader["email"],
                                                          reader["first_name"],
                                                          reader["last_name"],
                                                          reader["content_bio"],
                                                          reader["image_source"],
                                                          reader["is_admin"],
                                                          reader["is_blocked"],
                                                          reader["created_at"]);
                            returnUser.Add(user);
                        }
                    }
                }

            }

            return returnUser;
        }

        public string GetUserById(int userId)
        {
            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM [dbo].[user] WHERE id = @id";

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    cmd.Connection = con;
                    // Add parameter to command
                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = userId;

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}",
                                                          reader["ID"],
                                                          reader["username"],
                                                          reader["password"],
                                                          reader["email"],
                                                          reader["first_name"],
                                                          reader["last_name"],
                                                          reader["content_bio"],
                                                          reader["image_source"],
                                                          reader["is_admin"],
                                                          reader["is_blocked"],
                                                          reader["created_at"]);
                        }
                    }
                }

            }

            return User;
        }

        public List<string> GetUserByName(string username)
        {
            List<string> returnUser = new List<string>();
            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM [dbo].[user] WHERE username LIKE '%' + @username + '%'";

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    cmd.Connection = con;
                    // Add parameter to command
                    cmd.Parameters.Add("@username", SqlDbType.VarChar);
                    cmd.Parameters["@username"].Value = username;

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string user = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}",
                                                          reader["ID"],
                                                          reader["username"],
                                                          reader["password"],
                                                          reader["email"],
                                                          reader["first_name"],
                                                          reader["last_name"],
                                                          reader["content_bio"],
                                                          reader["image_source"],
                                                          reader["is_admin"],
                                                          reader["is_blocked"],
                                                          reader["created_at"]);
                            returnUser.Add(user);
                        }
                    }
                }

            }

            return returnUser;
        }

        public List<string> GetUserByStatus(string parameter)
        {
            List<string> returnUser = new List<string>();

            if (parameter.Trim().ToLower() == "admin")
            {
                parameter = "is_admin";
            }
            else if (parameter.Trim().ToLower() == "blocked")
            {
                parameter = "is_blocked";
            }

            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM [dbo].[user] WHERE is_blocked = 1";

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    cmd.Connection = con;

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string user = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}",
                                                          reader["ID"],
                                                          reader["username"],
                                                          reader["password"],
                                                          reader["email"],
                                                          reader["first_name"],
                                                          reader["last_name"],
                                                          reader["content_bio"],
                                                          reader["image_source"],
                                                          reader["is_admin"],
                                                          reader["is_blocked"],
                                                          reader["created_at"]);
                            returnUser.Add(user);
                        }
                    }
                }

            }

            return returnUser;
        }
    }
}
