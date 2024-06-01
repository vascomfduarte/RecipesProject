using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Assembly.RecipeApp.Repository.Repos
{
    public class CommentRepository : ICommentRepository
    {
        private static string _connectionString = ConnectionStringProvider.GetConnectionString();

        public Comment Comment;

        IUserRepository _userRepository;
        IRecipeRepository _recipeRepository;

        public CommentRepository(IUserRepository userRepository, IRecipeRepository recipeRepository, ICategoryRepository categoryRepository)
        {
            _userRepository = userRepository;
            _recipeRepository = recipeRepository;
        }

        public List<Comment> GetAll()
        {
            List<Comment> comments = new List<Comment>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM comment;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string body = reader.GetString(1);
                            Recipe recipe = _recipeRepository.GetById(reader.GetInt32(2)); // Assuming recipe_id column
                            int userId = reader.GetInt32(3); // Assuming user_id column
                            DateTime createdDate = reader.GetDateTime(4);

                            // Fetch user from database or create a method to do so
                            User user = _userRepository.GetById(userId);

                            comments.Add(new Comment(id, body, recipe, user, createdDate));
                        }
                    }
                }
            }

            return comments;
        } // Feito

        public Comment GetById(int id)
        {
            Comment comment = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM comment WHERE id = @id;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int commentId = reader.GetInt32(0);
                            string body = reader.GetString(1);
                            Recipe recipe = _recipeRepository.GetById(reader.GetInt32(2)); // Assuming recipe_id column
                            int userId = reader.GetInt32(3); // Assuming user_id column
                            DateTime createdDate = reader.GetDateTime(4);

                            // Fetch user from database or create a method to do so
                            User user = _userRepository.GetById(userId);

                            comment = new Comment(commentId, body, recipe, user, createdDate);
                        }
                    }
                }
            }

            return comment;
        } // Feito

        public List<Comment> GetByRecipeId(int recipeId)
        {
            List<Comment> comments = new List<Comment>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM comment WHERE recipe_id = @recipeId;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@recipeId", recipeId);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string body = reader.GetString(1);
                            Recipe recipe = _recipeRepository.GetById(reader.GetInt32(2)); // Assuming recipe_id column
                            int userId = reader.GetInt32(3); // Assuming user_id column
                                                             // Fetch recipe from database or create a method to do so
                            DateTime createdDate = reader.GetDateTime(4);
                                                        
                            // Fetch user from database or create a method to do so
                            User user = _userRepository.GetById(userId);

                            comments.Add(new Comment(id, body, recipe, user, createdDate));
                        }
                    }
                }
            }

            return comments;
        } // Feito

        public List<Comment> GetByUserId(int userId)
        {
            List<Comment> comments = new List<Comment>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM comment WHERE user_id = @userId;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string body = reader.GetString(1);
                            Recipe recipe = _recipeRepository.GetById(reader.GetInt32(2));
                            User user = _userRepository.GetById(reader.GetInt32(3));
                            DateTime createdDate = reader.GetDateTime(4);

                            comments.Add(new Comment(id, body, recipe, user, createdDate));
                        }
                    }
                }
            }

            return comments;
        } // Feito

        public bool Add(Comment comment)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO [dbo].[comment] (body, recipe_id, user_id, created_date)
                         VALUES (@body, @recipeId, @userId, @createdDate)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("@body", comment.Body);
                    cmd.Parameters.AddWithValue("@recipeId", comment.Recipe.Id);
                    cmd.Parameters.AddWithValue("@userId", comment.User.Id);
                    cmd.Parameters.AddWithValue("@createdDate", DateTime.UtcNow);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        } // Feito

        public bool Delete(Comment comment)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM comment WHERE id = @id;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", comment.Id);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        } // Feito

        public Comment Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Comment comment)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE [dbo].[comment]
                                 SET body = @body
                                 WHERE id = @commentId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("@body", comment.Body);
                    cmd.Parameters.AddWithValue("@commentId", comment.Id);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        } // Feito

    }
}
