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

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Parse the userData string to extract individual properties
                    string[] userData = userString.Split('|');

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

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Construct the string representation of the user
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

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameter to command
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = userId;

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Construct the string representation of the user
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
                string query = "SELECT * FROM [dbo].[user] WHERE LOWER([username]) LIKE '%' + @username + '%' OR LOWER([first_name]) LIKE '%' + @username + '%'";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameter to command
                    cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username.ToLower();

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Construct the string representation of the user
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

        public bool Update(string userString)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                // Parse the userData string to extract individual properties
                string[] userData = userString.Split('|');

                string query = "UPDATE [dbo].[user] SET [username] = @username, [password] = @password, [email] = @email, [first_name] = @firstName, " +
                               "[last_name] = @lastName, [content_bio] = @contentBio, [image_source] = @imageSource WHERE [ID] = @userId";
                
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameter to command
                    cmd.Parameters.AddWithValue("@userId", userData[0]);
                    cmd.Parameters.AddWithValue("@username", userData[1]);
                    cmd.Parameters.AddWithValue("@password", userData[2]);
                    cmd.Parameters.AddWithValue("@email", userData[3]);
                    cmd.Parameters.AddWithValue("@firstName", userData[4]);
                    cmd.Parameters.AddWithValue("@lastName", userData[5]);
                    cmd.Parameters.AddWithValue("@contentBio", userData[6]);
                    cmd.Parameters.AddWithValue("@imageSource", userData[7]);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }
    
        public bool UpdateIsAdmin(string userString)
        {
            throw new NotImplementedException();
        }
        public bool UpdateIsBlocked(string userString)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();

        }

    }
}
