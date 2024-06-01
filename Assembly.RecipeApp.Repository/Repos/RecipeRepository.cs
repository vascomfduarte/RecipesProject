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

        public List<Recipe> GetByUserId(int userId)
        {
            List<Recipe> recipes = new List<Recipe>();

            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM [dbo].[recipe] AS r " +
                               "INNER JOIN [dbo].[user] AS u ON r.[user_id] = u.[id] " +
                               "INNER JOIN [dbo].[difficulty] AS d ON r.[difficulty_id] = d.[id] " +
                               "WHERE r.[user_id] = @userId;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameter to command
                    cmd.Parameters.AddWithValue("@userId", userId);

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

        public List<Recipe> GetTopRatedRecipes(int count)
        {
            List<Recipe> recipes = new List<Recipe>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"
                                SELECT TOP (@Count) r.id, r.title, r.description, r.image_source, r.minutes_to_cook, r.is_approved, r.created_date, 
                                       u.id AS user_id, u.username, u.password, u.email, u.first_name, u.last_name, u.content_bio, 
                                       u.image_source AS user_image_source, u.is_admin, u.is_blocked, u.created_date AS user_created_date, 
                                       d.id AS difficulty_id, d.name AS difficulty_name, d.created_date AS difficulty_created_date,
                                       AVG(rt.value) AS average_rating,
                                       c.id AS category_id, c.name AS category_name, c.created_date AS category_created_date
                                FROM [dbo].[recipe] AS r
                                INNER JOIN [dbo].[user] AS u ON r.user_id = u.id
                                INNER JOIN [dbo].[difficulty] AS d ON r.difficulty_id = d.id
                                LEFT JOIN [dbo].[rating] AS rt ON r.id = rt.recipe_id
                                LEFT JOIN [dbo].[recipe_categories] AS rc ON r.id = rc.recipe_id
                                LEFT JOIN [dbo].[category] AS c ON rc.category_id = c.id
                                GROUP BY r.id, r.title, r.description, r.image_source, r.minutes_to_cook, r.is_approved, r.created_date, 
                                         u.id, u.username, u.password, u.email, u.first_name, u.last_name, u.content_bio, 
                                         u.image_source, u.is_admin, u.is_blocked, u.created_date, 
                                         d.id, d.name, d.created_date,
                                         c.id, c.name, c.created_date
                                ORDER BY average_rating DESC, r.created_date DESC";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Count", count);

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
                            User user = new User(reader.GetInt32(7),
                                                 reader.GetString(8),
                                                 reader.GetString(9),
                                                 reader.GetString(10),
                                                 reader.GetString(11),
                                                 reader.GetString(12),
                                                 reader.GetString(13),
                                                 reader.GetString(14),
                                                 reader.GetInt32(15) == 1 ? true : false,
                                                 reader.GetInt32(16) == 1 ? true : false,
                                                 reader.GetDateTime(17));

                            // Difficulty
                            Difficulty difficulty = new Difficulty(reader.GetInt32(18),
                                                                   reader.GetString(19),
                                                                   reader.GetDateTime(20));

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
        }  // Feito

        public bool Add(Recipe recipe)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO [dbo].[recipe] (title, description, image_source, minutes_to_cook, is_approved, created_date, user_id, difficulty_id)
                         VALUES (@title, @description, @imageSource, @minutesToCook, @isApproved, @createdDate, @userId, @difficultyId)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters to command
                    cmd.Parameters.AddWithValue("@title", recipe.Title);
                    cmd.Parameters.AddWithValue("@description", recipe.Description);
                    cmd.Parameters.AddWithValue("@imageSource", recipe.ImageSource ?? "");
                    cmd.Parameters.AddWithValue("@minutesToCook", recipe.MinutesToCook);
                    cmd.Parameters.AddWithValue("@isApproved", recipe.IsApproved is true ? 1 : 0);
                    cmd.Parameters.AddWithValue("@createdDate", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@userId", recipe.User.Id);
                    cmd.Parameters.AddWithValue("@difficultyId", recipe.Difficulty.Id);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        } // Feito

        public bool Update(Recipe entity)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE [dbo].[recipe] SET 
                                    title = @title,
                                    description = @description,
                                    image_source = @imageSource,
                                    minutes_to_cook = @minutesToCook,
                                    is_approved = @isApproved,
                                    user_id = @userId,
                                    difficulty_id = @difficultyId
                                 WHERE id = @id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters to command
                    cmd.Parameters.AddWithValue("@id", entity.Id);
                    cmd.Parameters.AddWithValue("@title", entity.Title);
                    cmd.Parameters.AddWithValue("@description", entity.Description);
                    cmd.Parameters.AddWithValue("@imageSource", entity.ImageSource ?? "");  // Can be null
                    cmd.Parameters.AddWithValue("@minutesToCook", entity.MinutesToCook);
                    cmd.Parameters.AddWithValue("@isApproved", entity.IsApproved ? 1 : 0);
                    cmd.Parameters.AddWithValue("@userId", entity.User.Id);                 // Assuming User.Id is the foreign key
                    cmd.Parameters.AddWithValue("@difficultyId", entity.Difficulty.Id);     // Assuming Difficulty.Id is the foreign key

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        } // Feito

        public bool Delete(Recipe entity)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM [dbo].[recipe] WHERE id = @id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameter to command
                    cmd.Parameters.AddWithValue("@id", entity.Id); // Assuming entity.Id exists

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        } // Feito

        public Recipe Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
