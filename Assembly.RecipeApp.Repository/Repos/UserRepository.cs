using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace Assembly.RecipeApp.Repository.Repos
{
    public class UserRepository : IUserRepository
    {
        private static string _connectionString = ConnectionStringProvider.GetConnectionString();

        public User User;

        public User Add(User entity)
        {
            throw new NotImplementedException();
        }        
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
        } // Para eliminar

        public List<User> GetAll()
        {
            List<User> users = new List<User>();

            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM [dbo].[user];";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string username = reader.GetString(1);
                            string password = reader.GetString(2);
                            string email = reader.GetString(3);
                            string firstName = reader.GetString(4);
                            string lastName = reader.GetString(5);
                            string contentBio = reader.GetString(6);
                            string imageSource = reader.GetString(7);
                            bool isAdmin = reader.GetBoolean(8);
                            bool isBlocked = reader.GetBoolean(9);
                            DateTime createdDate = reader.GetDateTime(10);

                            var user = new User(id, username, password, email, firstName, lastName, contentBio, imageSource, isAdmin, isBlocked, createdDate);

                            users.Add(user);
                        }
                    }
                }

            }

            return users;
        } // Feito 
        public User GetById(int userId)
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
                            int id = reader.GetInt32(0);
                            string username = reader.GetString(1);
                            string password = reader.GetString(2);
                            string email = reader.GetString(3);
                            string firstName = reader.GetString(4);
                            string lastName = reader.GetString(5);
                            string contentBio = reader.GetString(6);
                            string imageSource = reader.GetString(7);
                            bool isAdmin = reader.GetBoolean(8);
                            bool isBlocked = reader.GetBoolean(9);
                            DateTime createdDate = reader.GetDateTime(10);

                            User = new User(id, username, password, email, firstName, lastName, contentBio, imageSource, isAdmin, isBlocked, createdDate);                            
                        }
                    }
                }
            }

            return User;
        } // Feito
        public List<User> GetFilteredUsers(string input)
        {
            List<User> users = new List<User>();

            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM [dbo].[user] WHERE LOWER([username]) LIKE '%' + @input + '%' OR LOWER([first_name]) LIKE '%' + @input + '%'";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string username = reader.GetString(1);
                            string password = reader.GetString(2);
                            string email = reader.GetString(3);
                            string firstName = reader.GetString(4);
                            string lastName = reader.GetString(5);
                            string contentBio = reader.GetString(6);
                            string imageSource = reader.GetString(7);
                            bool isAdmin = reader.GetBoolean(8);
                            bool isBlocked = reader.GetBoolean(9);
                            DateTime createdDate = reader.GetDateTime(10);

                            var user = new User(id, username, password, email, firstName, lastName, contentBio, imageSource, isAdmin, isBlocked, createdDate);

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        } // Feito

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
        } // Para eliminar
        public User Update(User entity)
        {
            throw new NotImplementedException();
        }
        public bool UpdateBlockStatus(User user)
        {
            throw new NotImplementedException();
        }
        public bool UpdateAdminStatus(User user)
        {
            throw new NotImplementedException();
        }

        public User Delete(User entity)
        {
            throw new NotImplementedException();
        }
        public User Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
