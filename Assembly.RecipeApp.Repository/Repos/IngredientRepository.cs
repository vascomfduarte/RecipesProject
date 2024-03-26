using System.Data.SqlClient;
using System.Data;
using Assembly.RecipeApp.Repository.Interfaces;
using Assembly.RecipeApp.Domain.Model;

namespace Assembly.RecipeApp.Repository.Repos
{
    public class IngredientRepository : IIngredientRepository
    {
        private static string _connectionString = ConnectionStringProvider.GetConnectionString();

        public List<Ingredient> GetAll()
        {
            throw new NotImplementedException();
        }

        public Ingredient GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Ingredient> GetRecipeIngredients(int recipeId)
        {
            List<Ingredient> ingredients = new List<Ingredient>();

            // join de recipes com tabela de ingredients e amount
            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                // Refazer
                string query = "SELECT ri.[amount], i.[id] AS [product_id], i.[name] AS [product_name], ri.[unit_id]" +
                               " FROM [dbo].[ingredient] AS ri" +
                               " JOIN [dbo].[ingredient] AS i ON ri.[product_id] = i.[id]" +
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
                            int id = reader.GetInt32(0);
                            // Product
                            Product product = new Product(reader.GetString(1));
                            int amount = reader.GetInt32(2);
                            // Unit
                            Unit unit = new Unit(reader.GetInt32(3),
                                                 reader.GetString(4));

                            var ingredient = new Ingredient(id, product, amount, unit);

                            ingredients.Add(ingredient);
                        }
                    }
                }
            }

            return ingredients;
        }

        public Ingredient Add(Ingredient entity)
        {
            throw new NotImplementedException();
        }

        public Ingredient Update(Ingredient entity)
        {
            throw new NotImplementedException();
        }

        public Ingredient Delete(Ingredient entity)
        {
            throw new NotImplementedException();
        }
        public Ingredient Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
