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
    public class RatingRepository : IRatingRepository
    {
        private static string _connectionString = ConnectionStringProvider.GetConnectionString();

        public Rating Rating;

        public bool Add(Rating entity, User adminUser)
        {
            throw new NotImplementedException();
        }

        public bool Add(Rating entity, int recipeId, int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO [dbo].[rating] (value, recipe_id, user_id, created_date)
                         VALUES (@value, @recipeId, @userId, @createdDate)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("@value", entity.Value);
                    cmd.Parameters.AddWithValue("@recipeId", recipeId);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@createdDate", DateTime.UtcNow);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        } // Feito

        public bool Delete(int id, User adminUser)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByRecipeId(int recipeId)
        {
            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM [dbo].[rating] WHERE recipe_id = @id";

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

        public List<Rating> GetAll()
        {
            throw new NotImplementedException();
        }

        public Rating GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Rating> GetByRecipeId(int recipeId)
        {
            List<Rating> ratings = new List<Rating>();

            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                //string query = "SELECT * FROM category;";

                string query = "SELECT [id], [value], [created_date]" +
                               "FROM [dbo].[rating] WHERE [recipe_id] = @id;";


                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameter to command
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = recipeId;

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            int value = reader.GetInt32(1);
                            DateTime createdDate = reader.GetDateTime(2);

                            var rating = new Rating(id, value, createdDate);

                            ratings.Add(rating);
                        }
                    }
                }
            }

            return ratings;
        } // Feito

        public Rating GetByUserRecipeId(int recipeId, int userId)
        {
            Rating rating = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"SELECT [id], [value], [created_date]
                         FROM [dbo].[rating]
                         WHERE [recipe_id] = @recipeId AND [user_id] = @userId;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@recipeId", SqlDbType.Int).Value = recipeId;
                    cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            int value = reader.GetInt32(1);
                            DateTime createdDate = reader.GetDateTime(2);

                            rating = new Rating(id, value, createdDate);
                        }
                    }
                }
            }

            return rating;
        } // Feito

        public bool Update(Rating entity, User adminUser)
        {
            throw new NotImplementedException();
        }

        public bool Update(Rating entity, int recipeId, int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE [dbo].[rating] 
                        SET value = @value, 
                            created_date = @createdDate
                        WHERE recipe_id = @recipeId 
                        AND user_id = @userId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("@value", entity.Value);
                    cmd.Parameters.AddWithValue("@recipeId", recipeId);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@createdDate", DateTime.UtcNow);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        } // Feito

    }
}
