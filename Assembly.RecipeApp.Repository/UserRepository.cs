using System.Data;
using System.Data.SqlClient;

namespace Assembly.RecipeApp.Repository
{
    public class UserRepository
    {
        private static string _connectionString = ConnectionStringProvider.GetConnectionString();

        public string User;

        public bool Add(string userString)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO [dbo].[user] (username, password, email, first_name, last_name, content_bio, image_source, is_admin, is_blocked, created_at)
                             VALUES (@username, @password, @email, @firstName, @lastName, @contentBio, @imageSource, @isAdmin, @isBlocked, @created_at)";

                using (SqlCommand cmd = new SqlCommand())
                {
                    // Parse the userData string to extract individual properties
                    string[] userData = userString.Split('|');

                    cmd.CommandText = query;
                    cmd.Connection = con;
                    // Add parameter to command
                    cmd.Parameters.AddWithValue("@username", userData[0]);
                    cmd.Parameters.AddWithValue("@password", userData[1]);
                    cmd.Parameters.AddWithValue("@email", userData[2]);
                    cmd.Parameters.AddWithValue("@firstName", userData[3]);
                    cmd.Parameters.AddWithValue("@lastName", userData[4]);
                    cmd.Parameters.AddWithValue("@contentBio", userData[5]);
                    cmd.Parameters.AddWithValue("@imageSource", userData[6]);
                    cmd.Parameters.AddWithValue("@isAdmin", userData[7]);
                    cmd.Parameters.AddWithValue("@isBlocked", userData[8]);
                    cmd.Parameters.AddWithValue("@created_at", DateTime.UtcNow);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }

        public List<string> GetAll()
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

        public string GetById(int userId)
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

        public List<string> GetFilteredUsers(string username)
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

        public void Update(string entity)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();

        }

    }
}
