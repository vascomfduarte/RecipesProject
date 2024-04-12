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

namespace Assembly.RecipeApp.Repository.Repos
{
    public class PreparationMethodRepository : IPreparationMethodRepository
    {
        private static string _connectionString = ConnectionStringProvider.GetConnectionString();

        List<PreparationStep> PreparationSteps;

        public PreparationMethod GetByRecipeId(int recipeId)
        {
            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM preparation_step WHERE recipe_id = @id";

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
                            int order = reader.GetInt32(0);
                            string description = reader.GetString(1);

                            var preparationStep = new PreparationStep(order, description);

                            PreparationSteps.Add(preparationStep);                            
                        }
                    }
                }
            }

            return new PreparationMethod(PreparationSteps);
        }
    }
}
