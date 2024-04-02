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
        IRatingRepository _ratingRepository;
        ICategoryRepository _categoryRepository;

        public RecipeRepository(IIngredientRepository ingredientRepository, IRatingRepository ratingRepository, ICategoryRepository categoryRepository)
        {
            _ingredientRepository = ingredientRepository;
            _ratingRepository = ratingRepository;
            _categoryRepository = categoryRepository;
        }

        public List<Recipe> GetAll()
        {
            List<Recipe> recipes = new List<Recipe>();

            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM [dbo].[recipe] AS r " +
                               "INNER JOIN [dbo].[user] AS u ON r.[user_id] = u.[id] " +
                               "INNER JOIN [dbo].[difficulty] AS d ON r.[difficulty_id] = d.[id];";

                //string query = "SELECT r.[id], r.[title], r.[description], r.[image_source], r.[minutes_to_cook], r.[is_approved], r.[created_date], " +
                //               "u.[id] AS [user_id], u.[username], u.[password], u.[email], u.[first_name], u.[last_name], u.[content_bio], u.[image_source], " +
                //               "u.[is_admin], u.[is_blocked], u.[created_date], " +
                //               "d.[id] as [difficulty_id], d.[name], d.[created_date] " +
                //               "FROM [dbo].[recipe] AS r " +
                //               "INNER JOIN [dbo].[user] AS u ON r.[user_id] = u.[id] " +
                //               "INNER JOIN [dbo].[difficulty] AS d ON r.[difficulty_id] = d.[id];";


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
                            string description = reader.GetString(2);
                            string imageSource = reader.GetString(3);
                            int minutesToCook = reader.GetInt32(4);
                            bool isApproved = reader.GetInt32(5) == 1 ? true : false;
                            DateTime createdDate = reader.GetDateTime(6);

                            // User
                            User user = new User(reader.GetInt32(9),
                                                 reader.GetString(10),
                                                 reader.GetString(11),
                                                 reader.GetString(12),
                                                 reader.GetString(13),
                                                 reader.GetString(14),
                                                 reader.GetString(15),
                                                 reader.GetString(16),
                                                 reader.GetInt32(17) == 1 ? true : false,
                                                 reader.GetInt32(18) == 1 ? true : false,
                                                 reader.GetDateTime(19));

                            // Difficulty
                            Difficulty difficulty = new Difficulty(reader.GetInt32(20),
                                                                   reader.GetString(21),
                                                                   reader.GetDateTime(22));

                            // Rating List
                            List<Rating> ratings = _ratingRepository.GetByRecipeId(id);

                            // Category List
                            List<Category> categories = _categoryRepository.GetByRecipeId(id);

                            var recipe = new Recipe(id, title, description, imageSource, minutesToCook, isApproved, user, difficulty, ratings, categories, user.Username, createdDate);

                            recipes.Add(recipe);
                        }
                    }
                }
            }
            return recipes;
        } // Feito

        public Recipe GetById(int recipeId)
        {
            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                //string query = "SELECT * FROM recipe WHERE id = @id";

                string query = "SELECT * FROM [dbo].[recipe] AS r " +
                               "INNER JOIN [dbo].[user] AS u ON r.[user_id] = u.[id] " +
                               "INNER JOIN [dbo].[difficulty] AS d ON r.[difficulty_id] = d.[id] " +
                               "WHERE r.[id] = @id;";

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
                            string description = reader.GetString(2);
                            string imageSource = reader.GetString(3);
                            int minutesToCook = reader.GetInt32(4);
                            bool isApproved = reader.GetInt32(5) == 1 ? true : false;
                            DateTime createdDate = reader.GetDateTime(6);

                            // User
                            User user = new User(reader.GetInt32(9),
                                                 reader.GetString(10),
                                                 reader.GetString(11),
                                                 reader.GetString(12),
                                                 reader.GetString(13),
                                                 reader.GetString(14),
                                                 reader.GetString(15),
                                                 reader.GetString(16),
                                                 reader.GetInt32(17) == 1 ? true : false,
                                                 reader.GetInt32(18) == 1 ? true : false,
                                                 reader.GetDateTime(19));

                            // Difficulty
                            Difficulty difficulty = new Difficulty(reader.GetInt32(20),
                                                                   reader.GetString(21),
                                                                   reader.GetDateTime(22));

                            // Rating List
                            List<Rating> ratings = _ratingRepository.GetByRecipeId(id);

                            // Category List
                            List<Category> categories = _categoryRepository.GetByRecipeId(id);

                            Recipe = new Recipe(id, title, description, imageSource, minutesToCook, isApproved, user, difficulty, ratings, categories, user.Username, createdDate);
                        }
                    }
                }
            }

            return Recipe;
        } // Feito

        /// <summary>
        /// Method that searches for a given name
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public List<Recipe> GetFilteredRecipes(string searchTerm)
        {
            List<Recipe> recipes = new List<Recipe>();

            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM [dbo].[recipe] AS r " +
                               "INNER JOIN [dbo].[user] AS u ON r.[user_id] = u.[id] " +
                               "INNER JOIN [dbo].[difficulty] AS d ON r.[difficulty_id] = d.[id] " +
                               "WHERE r.[title] LIKE '%' + @searchTerm + '%';";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameter to command
                    cmd.Parameters.Add("@searchTerm", SqlDbType.NVarChar).Value = searchTerm.ToLower();

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string title = reader.GetString(1);
                            string description = reader.GetString(2);
                            string imageSource = reader.GetString(3);
                            int minutesToCook = reader.GetInt32(4);
                            bool isApproved = reader.GetInt32(5) == 1 ? true : false;
                            DateTime createdDate = reader.GetDateTime(6);

                            // User
                            User user = new User(reader.GetInt32(9),
                                                 reader.GetString(10),
                                                 reader.GetString(11),
                                                 reader.GetString(12),
                                                 reader.GetString(13),
                                                 reader.GetString(14),
                                                 reader.GetString(15),
                                                 reader.GetString(16),
                                                 reader.GetInt32(17) == 1 ? true : false,
                                                 reader.GetInt32(18) == 1 ? true : false,
                                                 reader.GetDateTime(19));

                            // Difficulty
                            Difficulty difficulty = new Difficulty(reader.GetInt32(20),
                                                                   reader.GetString(21),
                                                                   reader.GetDateTime(22));

                            // Rating List
                            List<Rating> ratings = _ratingRepository.GetByRecipeId(id);

                            // Category List
                            List<Category> categories = _categoryRepository.GetByRecipeId(id);

                            var recipe = new Recipe(id, title, description, imageSource, minutesToCook, isApproved, user, difficulty, ratings, categories, user.Username, createdDate);

                            recipes.Add(recipe);
                        }
                    }
                }
            }

            return recipes;
        } // Feito

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
