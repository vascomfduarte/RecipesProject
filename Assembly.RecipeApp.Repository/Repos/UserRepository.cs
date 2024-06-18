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

        public bool Add(User user)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO [dbo].[user] (username, password, email, first_name, last_name, content_bio, image_source, is_admin, is_blocked, created_date)
                             VALUES (@username, @password, @email, @firstName, @lastName, @contentBio, @imageSource, @isAdmin, @isBlocked, @createdDate)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameter to command
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@firstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", user.LastName);
                    cmd.Parameters.AddWithValue("@contentBio", user.ContentBio ?? ""); // Assuming contentBio can be null
                    cmd.Parameters.AddWithValue("@imageSource", user.ImageSource ?? ""); // Assuming imageSource can be null
                    cmd.Parameters.AddWithValue("@isAdmin", user.IsAdmin is true ? 1 : 0);
                    cmd.Parameters.AddWithValue("@isBlocked", user.IsBlocked is true ? 1 : 0);
                    cmd.Parameters.AddWithValue("@createdDate", DateTime.UtcNow);


                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        } // Feito 

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
                            bool isAdmin = reader.GetInt32(8) == 1 ? true : false;
                            bool isBlocked = reader.GetInt32(9) == 1 ? true : false;
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
                            bool isAdmin = reader.GetInt32(8) == 1 ? true : false;
                            bool isBlocked = reader.GetInt32(9) == 1 ? true : false;
                            DateTime createdDate = reader.GetDateTime(10);

                            User = new User(id, username, password, email, firstName, lastName, contentBio, imageSource, isAdmin, isBlocked, createdDate);                            
                        }
                    }
                }
            }

            return User;
        } // Feito
        public User GetByUsername(string username)
        {
            User user = null;

            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM [dbo].[user] WHERE username = @username";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameter to command
                    cmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string fetchedUsername = reader.GetString(1);
                            string password = reader.GetString(2);
                            string email = reader.GetString(3);
                            string firstName = reader.GetString(4);
                            string lastName = reader.GetString(5);
                            string contentBio = reader.GetString(6);
                            string imageSource = reader.GetString(7);
                            bool isAdmin = reader.GetInt32(8) == 1;
                            bool isBlocked = reader.GetInt32(9) == 1;
                            DateTime createdDate = reader.GetDateTime(10);

                            user = new User(id, fetchedUsername, password, email, firstName, lastName, contentBio, imageSource, isAdmin, isBlocked, createdDate);
                        }
                    }
                }
            }

            return user;
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
                            bool isAdmin = reader.GetInt32(8) == 1 ? true : false;
                            bool isBlocked = reader.GetInt32(9) == 1 ? true : false;
                            DateTime createdDate = reader.GetDateTime(10);

                            var user = new User(id, username, password, email, firstName, lastName, contentBio, imageSource, isAdmin, isBlocked, createdDate);

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        } // Feito
        public List<User> GetUsers(int currentPage, int pageSize)
        {
            List<User> users = new List<User>();

            // Calculate the offset based on the current page and page size
            int offset = (currentPage - 1) * pageSize;

            // Collect data from the database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"
                                SELECT * FROM (
                                    SELECT *, ROW_NUMBER() OVER (ORDER BY [id]) AS RowNum
                                    FROM [dbo].[user]
                                ) AS Temp
                                WHERE RowNum BETWEEN @Offset AND @EndRow";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Set the pagination parameters
                    cmd.Parameters.AddWithValue("@Offset", offset + 1);
                    cmd.Parameters.AddWithValue("@EndRow", offset + pageSize);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Read user data from the reader
                            int id = reader.GetInt32(0);
                            string username = reader.GetString(1);
                            string password = reader.GetString(2);
                            string email = reader.GetString(3);
                            string firstName = reader.GetString(4);
                            string lastName = reader.GetString(5);
                            string contentBio = reader.GetString(6);
                            string imageSource = reader.GetString(7);
                            bool isAdmin = reader.GetInt32(8) == 1;
                            bool isBlocked = reader.GetInt32(9) == 1;
                            DateTime createdDate = reader.GetDateTime(10);

                            // Create a User object and add it to the list
                            var user = new User(id, username, password, email, firstName, lastName, contentBio, imageSource, isAdmin, isBlocked, createdDate);
                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        } // Feito
        public List<User> GetBlockedUsers(int currentPage, int pageSize)
        {
            List<User> users = new List<User>();

            // Calculate the offset based on the current page and page size
            int offset = (currentPage - 1) * pageSize;

            // Collect data from the database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"
                                SELECT * FROM (
                                    SELECT *, ROW_NUMBER() OVER (ORDER BY [id]) AS RowNum
                                    FROM [dbo].[user]
                                    WHERE is_blocked = 1 -- Filter only blocked users
                                ) AS Temp
                                WHERE RowNum BETWEEN @Offset AND @EndRow";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Set the pagination parameters
                    cmd.Parameters.AddWithValue("@Offset", offset + 1);
                    cmd.Parameters.AddWithValue("@EndRow", offset + pageSize);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Read user data from the reader
                            int id = reader.GetInt32(0);
                            string username = reader.GetString(1);
                            string password = reader.GetString(2);
                            string email = reader.GetString(3);
                            string firstName = reader.GetString(4);
                            string lastName = reader.GetString(5);
                            string contentBio = reader.GetString(6);
                            string imageSource = reader.GetString(7);
                            bool isAdmin = reader.GetInt32(8) == 1;
                            bool isBlocked = reader.GetInt32(9) == 1;
                            DateTime createdDate = reader.GetDateTime(10);

                            // Create a User object and add it to the list
                            var user = new User(id, username, password, email, firstName, lastName, contentBio, imageSource, isAdmin, isBlocked, createdDate);
                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        } // Feito

        public bool Update(User entity)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE [dbo].[user] SET 
                                    password = @password,
                                    email = @email,
                                    first_name = @firstName,
                                    last_name = @lastName,
                                    content_bio = @contentBio,
                                    image_source = @imageSource,
                                    is_admin = @isAdmin,
                                    is_blocked = @isBlocked
                                 WHERE username = @username";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters to command
                    cmd.Parameters.AddWithValue("@username", entity.Username);
                    cmd.Parameters.AddWithValue("@password", entity.Password);
                    cmd.Parameters.AddWithValue("@email", entity.Email);
                    cmd.Parameters.AddWithValue("@firstName", entity.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", entity.LastName);
                    cmd.Parameters.AddWithValue("@contentBio", entity.ContentBio ?? ""); // Can be null
                    cmd.Parameters.AddWithValue("@imageSource", entity.ImageSource ?? ""); // Can be null
                    cmd.Parameters.AddWithValue("@isAdmin", entity.IsAdmin ? 1 : 0);
                    cmd.Parameters.AddWithValue("@isBlocked", entity.IsBlocked ? 1 : 0);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        } // Feito

        public bool UpdateById(User entity)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE [dbo].[user] SET 
                            username = @username,
                            password = @password,
                            email = @email,
                            first_name = @firstName,
                            last_name = @lastName,
                            content_bio = @contentBio,
                            image_source = @imageSource,
                            is_admin = @isAdmin,
                            is_blocked = @isBlocked
                         WHERE id = @id";  // Update based on ID

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters to command
                    cmd.Parameters.AddWithValue("@id", entity.Id);  // Add ID parameter
                    cmd.Parameters.AddWithValue("@username", entity.Username);  // Update username
                    cmd.Parameters.AddWithValue("@password", entity.Password);
                    cmd.Parameters.AddWithValue("@email", entity.Email);
                    cmd.Parameters.AddWithValue("@firstName", entity.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", entity.LastName);
                    cmd.Parameters.AddWithValue("@contentBio", entity.ContentBio ?? ""); // Can be null
                    cmd.Parameters.AddWithValue("@imageSource", entity.ImageSource ?? ""); // Can be null
                    cmd.Parameters.AddWithValue("@isAdmin", entity.IsAdmin ? 1 : 0);
                    cmd.Parameters.AddWithValue("@isBlocked", entity.IsBlocked ? 1 : 0);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        } // Feito

        public bool Delete(User entity)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"DELETE FROM [dbo].[user] WHERE username = @username";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", entity.Username);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        } // Feito

        public User Login(string inputUsername, string inputPassword)
        {
            User user = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM [dbo].[user] WHERE [username] = @Username AND [password] = @Password;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", inputUsername);
                    cmd.Parameters.AddWithValue("@Password", inputPassword);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string username = reader.GetString(1);
                            string password = reader.GetString(2);
                            string email = reader.GetString(3);
                            string firstName = reader.GetString(4);
                            string lastName = reader.GetString(5);
                            string contentBio = reader.GetString(6);
                            string imageSource = reader.GetString(7);
                            bool isAdmin = reader.GetInt32(8) == 1 ? true : false;
                            bool isBlocked = reader.GetInt32(9) == 1 ? true : false;
                            DateTime createdDate = reader.GetDateTime(10);

                            user = new User(id, username, password, email, firstName, lastName, contentBio, imageSource, isAdmin, isBlocked, createdDate);
                        }
                    }
                }
            }

            return user;
        } // Feito

        public User Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
