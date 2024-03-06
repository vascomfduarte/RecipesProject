using System.Data.SqlClient;
using System.Data;

namespace Assembly.RecipeApp.Repository
{
    public class IngredientRepository
    {
        private static string _connectionString = ConnectionStringProvider.GetConnectionString();

        List<string> Ingredients = new List<string>();

        public bool Add(string ingredient)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAll()
        {
            throw new NotImplementedException();
        }

        public string GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<string> GetRecipeIngredients(int recipeId)
        {
            // join de recipes com tabela de ingredients e amount
            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT ri.[amount], i.[id] AS [ingredient_id], i.[name] AS [ingredient_name], ri.[unit_id]" +
                               " FROM [JD_FC_VD_RecipesProject].[dbo].[recipe_ingredients] AS ri" +
                               " JOIN [JD_FC_VD_RecipesProject].[dbo].[ingredient] AS i ON ri.[ingredient_id] = i.[id]" +
                               " WHERE ri.[recipe_id] = @recipeId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameter to command
                    cmd.Parameters.Add("@recipeId", SqlDbType.Int).Value = recipeId;

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Construct the string representation of the recipe
                            string ingredient = string.Format("{0}|{1}|{2}|{3}",
                                                reader["ingredient_id"],
                                                reader["ingredient_name"],
                                                reader["amount"],
                                                reader["unit_id"]);

                            Ingredients.Add(ingredient);
                        }
                    }
                }
            }

            return Ingredients;

            throw new NotImplementedException();
        }

        public List<string> GetIngredient(int recipeId)
        {
            throw new NotImplementedException();
        }
    }
}
