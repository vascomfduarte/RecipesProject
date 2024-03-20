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

                            var difficulty = new Difficulty(id, name);

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

                            Difficulty = new Difficulty(difficultyId, name);
                        }
                    }
                }
            }

            return Difficulty;
        } // Feito

        public bool Add(Difficulty entity, User adminUser)
        {
            throw new NotImplementedException();
        }

        public bool Update(Difficulty entity, User adminUser)
        {
            throw new NotImplementedException();
        }
        public bool Delete(int id, User adminUser)
        {
            throw new NotImplementedException();
        }

    }
}
