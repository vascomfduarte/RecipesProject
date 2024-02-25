using System.Data;
using System.Data.SqlClient;

namespace Repository
{
    public class RecipeRepository
	{
		private static string _connectionString = ConnectionStringProvider.GetConnectionString();

		public List<string> GetRecipes()
        {
            List<string> returnRecipes = new List<string>();
            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM recipe";

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    cmd.Connection = con;

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Construct the string representation of the recipe
                            string recipe = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}",
                                                          reader["ID"],
                                                          reader["title"],
                                                          reader["instructions"],
                                                          reader["image_source"],
                                                          reader["minutes_to_cook"],
                                                          reader["is_approved"],
                                                          reader["user_id"]);
                            returnRecipes.Add(recipe);
                        }
                    }
                }
            }

            return returnRecipes;
        }

		public List<string> GetRecipesByName(string title)
		{
			List<string> returnRecipes = new List<string>();
			// Collect data from database
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
                string query;

                if (title == "0")
                    query = "SELECT * FROM recipe";
                else
                    query = "SELECT * FROM recipe WHERE title LIKE '%' + @title + '%'";

				using (SqlCommand cmd = new SqlCommand())
				{
					cmd.CommandText = query;
					cmd.Connection = con;
					// Add parameter to command
					cmd.Parameters.Add("@title", SqlDbType.NVarChar);
					cmd.Parameters["@title"].Value = title;

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
