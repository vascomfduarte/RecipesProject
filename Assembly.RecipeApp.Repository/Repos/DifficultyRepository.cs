using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository.Interfaces;
using System.Data.SqlClient;
using System.Data;

namespace Assembly.RecipeApp.Repository.Repos
{
    public class DifficultyRepository : IDifficultyRepository
    {
        private static string _connectionString = ConnectionStringProvider.GetConnectionString();

        public Difficulty Difficulty;

        public List<Difficulty> GetAll()
        {
            List<Difficulty> difficulties = new List<Difficulty>();

            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM difficulty;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            DateTime date = reader.GetDateTime(2);

                            var difficulty = new Difficulty(id, name, date);

                            difficulties.Add(difficulty);
                        }
                    }
                }
            }

            return difficulties;
        } // Feito

        public Difficulty GetById(int id)
        {
            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM difficulty WHERE id = @id;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameter to command
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int difficultyId = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            DateTime date = reader.GetDateTime(2);

                            Difficulty = new Difficulty(difficultyId, name, date);
                        }
                    }
                }
            }

            return Difficulty;
        } // Feito

        public Difficulty GetByName(string name)
        {
            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM difficulty WHERE name = @name;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameter to command
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int difficultyId = reader.GetInt32(0);
                            string difficultyName = reader.GetString(1);
                            DateTime date = reader.GetDateTime(2);

                            Difficulty = new Difficulty(difficultyId, difficultyName, date);
                        }
                    }
                }
            }

            return Difficulty;
        } // Feito

        public bool Add(Difficulty entity)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO [dbo].[difficulty] (name, created_date)
                         VALUES (@name, @createdDate)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("@name", entity.Name);
                    cmd.Parameters.AddWithValue("@createdDate", DateTime.UtcNow);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        } // Feito

        public bool Update(Difficulty entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Difficulty entity)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"DELETE FROM [dbo].[difficulty] WHERE id = @id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", entity.Id);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        } // Feito

    }
}
