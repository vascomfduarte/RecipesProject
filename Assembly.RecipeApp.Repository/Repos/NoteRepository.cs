using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.RecipeApp.Repository.Repos
{
    public class NoteRepository : INoteRepository
    {
        private static string _connectionString = ConnectionStringProvider.GetConnectionString();

        public Note Note;

        IUserRepository _userRepository;
        IRecipeRepository _recipeRepository;

        public NoteRepository(IUserRepository userRepository, IRecipeRepository recipeRepository, ICategoryRepository categoryRepository)
        {
            _userRepository = userRepository;
            _recipeRepository = recipeRepository;
        }

        public List<Note> GetAll()
        {
            List<Note> notes = new List<Note>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM note;";

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

                            notes.Add(new Note(id, body, recipe, user, createdDate));
                        }
                    }
                }
            }

            return notes;
        } // Feito

        public Note GetById(int id)
        {
            Note note = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM note WHERE id = @id;";

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

                            note = new Note(commentId, body, recipe, user, createdDate);
                        }
                    }
                }
            }

            return note;
        } // Feito

        public List<Note> GetByRecipeId(int recipeId)
        {
            List<Note> notes = new List<Note>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM note WHERE recipe_id = @recipeId;";

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

                            notes.Add(new Note(id, body, recipe, user, createdDate));
                        }
                    }
                }
            }

            return notes;
        } // Feito

        public List<Note> GetByUserId(int userId)
        {
            List<Note> notes = new List<Note>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM note WHERE user_id = @userId;";

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

                            notes.Add(new Note(id, body, recipe, user, createdDate));
                        }
                    }
                }
            }

            return notes;
        } // Feito

        public bool Add(Note note)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO [dbo].[note] (body, recipe_id, user_id, created_date)
                         VALUES (@body, @recipeId, @userId, @createdDate)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("@body", note.Body);
                    cmd.Parameters.AddWithValue("@recipeId", note.Recipe.Id);
                    cmd.Parameters.AddWithValue("@userId", note.User.Id);
                    cmd.Parameters.AddWithValue("@createdDate", DateTime.UtcNow);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        } // Feito

        public bool Delete(Note note)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM note WHERE id = @id;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", note.Id);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        } // Feito

        public bool DeleteByRecipeId(int recipeId)
        {
            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM [dbo].[note] WHERE recipe_id = @id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameter to command
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = recipeId;

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        } // Feito

        public Note Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Note note)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE [dbo].[note]
                                 SET body = @body
                                 WHERE id = @noteId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("@body", note.Body);
                    cmd.Parameters.AddWithValue("@noteId", note.Id);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        } // Feito

    }
}
