using System.Data.SqlClient;
using System.Data;
using Assembly.RecipeApp.Repository.Interfaces;
using Assembly.RecipeApp.Domain.Model;

namespace Assembly.RecipeApp.Repository.Repos
{
    public class IngredientRepository : IIngredientRepository
    {
        private static string _connectionString = ConnectionStringProvider.GetConnectionString();

        private readonly IUnitRepository _unitRepository;

        public Ingredient Ingredient;

        public IngredientRepository(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public List<Ingredient> GetAll()
        {
            throw new NotImplementedException();
        }

        public Ingredient GetById(int id)
        {
            Ingredient ingredient = null;

            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"SELECT [ingredient_id], ri.[amount], 
                        p.[id] AS [product_id], 
                        p.[name] AS [product_name], 
                        ri.[unit_id],
                        ri.[created_date]
                FROM [dbo].[ingredient] AS ri
                JOIN [dbo].[product] AS p ON ri.[product_id] = p.[id]
                WHERE ri.[ingredient_id] = @id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameter to command
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int ingredientId = reader.GetInt32(0);
                            double amount = reader.GetDouble(1);

                            // Product
                            string productName = reader.GetString(3);
                            Product product = new Product(productName);

                            // Unit
                            int unitId = reader.GetInt32(4);

                            // Create Ingredient instance
                            ingredient = new Ingredient(ingredientId, product, amount, _unitRepository.GetById(unitId), DateTime.Now);
                        }
                    }
                }
            }

            return ingredient;
        }

        public List<Ingredient> GetRecipeIngredients(int recipeId)
        {
            List<Ingredient> ingredients = new List<Ingredient>();

            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"SELECT ri.[ingredient_id], ri.[amount], 
                        p.[id] AS [product_id], 
                        p.[name] AS [product_name], 
                        ri.[unit_id],
                        ri.[created_date]
                FROM [dbo].[ingredient] AS ri
                JOIN [dbo].[product] AS p ON ri.[product_id] = p.[id]
                WHERE ri.[recipe_id] = @recipeId";

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
                            int ingredientId = reader.GetInt32(0);
                            double amount = reader.GetDouble(1);

                            // Product
                            string productName = reader.GetString(3);
                            Product product = new Product(productName);

                            // Unit
                            int unitId = reader.GetInt32(4);

                            // Create Ingredient instance
                            Ingredient ingredient = new Ingredient(ingredientId, product, amount, _unitRepository.GetById(unitId), DateTime.Now);

                            ingredients.Add(ingredient);
                        }
                    }
                }
            }

            return ingredients;
        } // Feito

        public bool Add(Ingredient entity)
        {
            throw new NotImplementedException();
        }

        public bool Add(Ingredient ingredient, int recipeId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO [dbo].[ingredient] (product_id, amount, unit_id, recipe_id, created_date)
                    VALUES (@productId, @amount, @unitId, @recipeId, @createdDate)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("@productId", ingredient.Product.Id);
                    cmd.Parameters.AddWithValue("@amount", ingredient.Amount);
                    cmd.Parameters.AddWithValue("@unitId", ingredient.Unit.Id);
                    cmd.Parameters.AddWithValue("@recipeId", recipeId);
                    cmd.Parameters.AddWithValue("@createdDate", DateTime.UtcNow);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        } // Feito

        public bool Update(Ingredient entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByRecipeId(int recipeId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM [dbo].[ingredient] WHERE recipe_id = @id";

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

        public bool Delete(Ingredient entity)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM [dbo].[ingredient] WHERE ingredient_id = @id";

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

        public Ingredient Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
