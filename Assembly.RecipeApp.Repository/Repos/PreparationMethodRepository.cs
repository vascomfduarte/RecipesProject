using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace Assembly.RecipeApp.Repository.Repos
{
    public class PreparationMethodRepository : IPreparationMethodRepository
    {
        private static string _connectionString = ConnectionStringProvider.GetConnectionString();

        List<PreparationStep> PreparationSteps;

        public PreparationMethod GetByRecipeId(int recipeId)
        {
            List<PreparationStep> preparationSteps = new List<PreparationStep>();

            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT [id], [order], [description], [image_source], [created_date] " +
                               "FROM preparation_step WHERE recipe_id = @id";

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
                            int order = reader.GetInt32(1);
                            string description = reader.GetString(2);
                            string imageSource = !reader.IsDBNull(3) ? reader.GetString(3) : null;
                            DateTime createdDate = reader.GetDateTime(4);

                            var preparationStep = new PreparationStep(id, order, description, imageSource, createdDate);

                            preparationSteps.Add(preparationStep);
                        }
                    }
                }
            }

            return new PreparationMethod(preparationSteps);
        }

        public List<PreparationStep> GetStepsByRecipeId(int recipeId)
        {
            List<PreparationStep> preparationSteps = new List<PreparationStep>();

            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT [id], [order], [description], [image_source], [created_date] " +
                               "FROM preparation_step WHERE recipe_id = @id";

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
                            int order = reader.GetInt32(1);
                            string description = reader.GetString(2);
                            string imageSource = !reader.IsDBNull(3) ? reader.GetString(3) : null;
                            DateTime createdDate = reader.GetDateTime(4);

                            var preparationStep = new PreparationStep(id, order, description, imageSource, createdDate);

                            preparationSteps.Add(preparationStep);
                        }
                    }
                }
            }

            return preparationSteps;
        } // Feito

        public PreparationStep GetStepById(int stepId)
        {
            PreparationStep preparationStep = null;

            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT [id], [order], [description], [image_source], [created_date] " +
                               "FROM preparation_step WHERE id = @id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameter to command
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = stepId;

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            int order = reader.GetInt32(1);
                            string description = reader.GetString(2);
                            string imageSource = !reader.IsDBNull(3) ? reader.GetString(3) : null;
                            DateTime createdDate = reader.GetDateTime(4);

                            preparationStep = new PreparationStep(id, order, description, imageSource, createdDate);
                        }
                    }
                }
            }

            return preparationStep;
        } // Feito

        public bool Add(PreparationMethod entity, int recipeId)
        {
            throw new NotImplementedException();
        }

        public bool AddStep(PreparationStep entity, int recipeId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO [dbo].[preparation_step] 
                        ([recipe_id], [order], [description], [image_source], [created_date])
                        VALUES (@recipeId, @order, @description, @imageSource, @createdDate)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@recipeId", SqlDbType.Int).Value = recipeId;
                    cmd.Parameters.Add("@order", SqlDbType.Int).Value = entity.Order;
                    cmd.Parameters.Add("@description", SqlDbType.NVarChar).Value = entity.Description;
                    cmd.Parameters.Add("@imageSource", SqlDbType.NVarChar).Value = entity.ImageSource ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@createdDate", SqlDbType.DateTime).Value = entity.CreatedDate;

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        public bool DeleteByRecipeId(int recipeId)
        {
            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM preparation_step WHERE recipe_id = @id";

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

        public bool DeleteStep(PreparationStep entity)
        {
            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM preparation_step WHERE id = @id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameter to command
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = entity.Id;

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }

        } // Feito

    }
}
