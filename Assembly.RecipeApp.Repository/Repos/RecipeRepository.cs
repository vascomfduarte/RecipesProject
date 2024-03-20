using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace Assembly.RecipeApp.Repository.Repos
{
    public class RecipeRepository : IRecipeRepository        
    {    
        private static string _connectionString = ConnectionStringProvider.GetConnectionString();

        public Recipe Recipe;

        IIngredientRepository _ingredientRepository;

        public RecipeRepository(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public List<Recipe> GetAll()
        {
            List<Recipe> recipes = new List<Recipe>();

            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM recipe";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string title = reader.GetString(1);
                            string instructions = reader.GetString(2);
                            string imageSource = reader.GetString(3);
                            int minutesToCook = reader.GetInt32(4);
                            bool isApproved = reader.GetBoolean(5);
                            DateTime createdDate = reader.GetDateTime(19);

                            // User
                            User user = new User(reader.GetInt32(6), 
                                                 reader.GetString(7), 
                                                 reader.GetString(8), 
                                                 reader.GetString(9), 
                                                 reader.GetString(10), 
                                                 reader.GetString(11), 
                                                 reader.GetString(12), 
                                                 reader.GetString(13), 
                                                 reader.GetBoolean(14), 
                                                 reader.GetBoolean(15), 
                                                 reader.GetDateTime(16));
                            // Difficulty
                            Difficulty difficulty = new Difficulty(reader.GetInt32(17), 
                                                                   reader.GetString(18));
                            // Ingredients
                            List<Ingredient> ingredients = _ingredientRepository.GetRecipeIngredients(id);

                            //List<Comments> ingredients = ;
                            //List<Ratings> ingredients = ;
                            //List<Categories> ingredients = ;

                            var recipe = new Recipe(id, title, instructions, imageSource, minutesToCook, isApproved, user, difficulty, createdDate, ingredients);

                            recipes.Add(recipe);
                        }
                    }
                }
            }
            return recipes;
        }

        public Recipe GetById(int recipeId)
        {
            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM recipe WHERE id = @id";

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
                            string title = reader.GetString(1);
                            string instructions = reader.GetString(2);
                            string imageSource = reader.GetString(3);
                            int minutesToCook = reader.GetInt32(4);
                            bool isApproved = reader.GetBoolean(5);
                            DateTime createdDate = reader.GetDateTime(19);

                            // User
                            User user = new User(reader.GetInt32(6),
                                                 reader.GetString(7),
                                                 reader.GetString(8),
                                                 reader.GetString(9),
                                                 reader.GetString(10),
                                                 reader.GetString(11),
                                                 reader.GetString(12),
                                                 reader.GetString(13),
                                                 reader.GetBoolean(14),
                                                 reader.GetBoolean(15),
                                                 reader.GetDateTime(16));
                            // Difficulty
                            Difficulty difficulty = new Difficulty(reader.GetInt32(17),
                                                                   reader.GetString(18));
                            // Ingredients
                            List<Ingredient> ingredients = _ingredientRepository.GetRecipeIngredients(id);

                            //List<Comments> ingredients = ;
                            //List<Ratings> ingredients = ;
                            //List<Categories> ingredients = ;

                            Recipe = new Recipe(id, title, instructions, imageSource, minutesToCook, isApproved, user, difficulty, createdDate, ingredients);
                        }
                    }
                }
            }

            return Recipe;
        }

        /// <summary>
        /// Method that queries throw all recipes or for a given name
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public List<Recipe> GetFilteredRecipes(string name)
        {
            List<Recipe> recipes = new List<Recipe>();

            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM recipe WHERE title LIKE '%' + @name + '%'";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameter to command
                    cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = name.ToLower();

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string title = reader.GetString(1);
                            string instructions = reader.GetString(2);
                            string imageSource = reader.GetString(3);
                            int minutesToCook = reader.GetInt32(4);
                            bool isApproved = reader.GetBoolean(5);
                            DateTime createdDate = reader.GetDateTime(19);

                            // User
                            User user = new User(reader.GetInt32(6),
                                                 reader.GetString(7),
                                                 reader.GetString(8),
                                                 reader.GetString(9),
                                                 reader.GetString(10),
                                                 reader.GetString(11),
                                                 reader.GetString(12),
                                                 reader.GetString(13),
                                                 reader.GetBoolean(14),
                                                 reader.GetBoolean(15),
                                                 reader.GetDateTime(16));
                            // Difficulty
                            Difficulty difficulty = new Difficulty(reader.GetInt32(17),
                                                                   reader.GetString(18));
                            // Ingredients
                            List<Ingredient> ingredients = _ingredientRepository.GetRecipeIngredients(id);

                            //List<Comments> ingredients = ;
                            //List<Ratings> ingredients = ;
                            //List<Categories> ingredients = ;

                            var recipe = new Recipe(id, title, instructions, imageSource, minutesToCook, isApproved, user, difficulty, createdDate, ingredients);

                            recipes.Add(recipe);
                        }
                    }
                }
            }
            return recipes;

        }

        public Recipe Add(Recipe entity)
        {
            throw new NotImplementedException();
        }

        public Recipe Update(Recipe entity)
        {
            throw new NotImplementedException();
        }

        public Recipe Delete(Recipe entity)
        {
            throw new NotImplementedException();
        }

        public Recipe Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
