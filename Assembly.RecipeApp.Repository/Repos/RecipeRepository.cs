using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace Assembly.RecipeApp.Repository.Repos
{
    public class RecipeRepository
    {
        private static string _connectionString = ConnectionStringProvider.GetConnectionString();

        public string Recipe;

        public bool Add(string recipeString)
        {
            throw new NotImplementedException();
        }
        public List<string> GetAll()
        {
            List<string> returnRecipes = new List<string>();

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
                            // Construct the string representation of the recipe
                            string recipe = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
                                                            reader["ID"],
                                                            reader["title"],
                                                            reader["instructions"],
                                                            reader["image_source"],
                                                            reader["minutes_to_cook"],
                                                            reader["is_approved"],
                                                            reader["user_id"],
                                                            reader["difficulty_id"],
                                                            reader["created_at"]);
                            returnRecipes.Add(recipe);
                        }
                    }
                }
            }
            return returnRecipes;
        }

        public string GetById(int recipeId)
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
                            // Construct the string representation of the recipe
                            Recipe = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
                                                      reader["ID"],
                                                      reader["title"],
                                                      reader["instructions"],
                                                      reader["image_source"],
                                                      reader["minutes_to_cook"],
                                                      reader["is_approved"],
                                                      reader["user_id"],
                                                      reader["difficulty_id"],
                                                      reader["created_at"]);
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
        public List<string> GetFilteredRecipes(string title)
        {
            List<string> returnRecipes = new List<string>();

            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM recipe WHERE title LIKE '%' + @title + '%'";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameter to command
                    cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = title.ToLower();

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Construct the string representation of the recipe
                            string recipe = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
                                                            reader["ID"],
                                                            reader["title"],
                                                            reader["instructions"],
                                                            reader["image_source"],
                                                            reader["minutes_to_cook"],
                                                            reader["is_approved"],
                                                            reader["user_id"],
                                                            reader["difficulty_id"],
                                                            reader["created_at"]);
                            returnRecipes.Add(recipe);
                        }
                    }
                }
            }
            return returnRecipes;

        }
    }
}
