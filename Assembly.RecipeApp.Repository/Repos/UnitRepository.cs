
using System.Data.SqlClient;
using System.Data;
using Assembly.RecipeApp.Repository.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Domain.Interfaces;

namespace Assembly.RecipeApp.Repository.Repos
{
    public class UnitRepository : IUnitRepository
    {
        private static string _connectionString = ConnectionStringProvider.GetConnectionString();

        public Unit Unit;

        public List<Unit> GetAll()
        {
            List<Unit> units = new List<Unit>();

            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM unit;";

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
                            DateTime createdDate = reader.GetDateTime(2);

                            var unit = new Unit(id, name, createdDate);

                            units.Add(unit);
                        }
                    }
                }
            }

            return units;
        } // Feito

        public Unit GetById(int id)
        {
            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM unit WHERE id = @id;";

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
                            int unitId = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            DateTime createdDate = reader.GetDateTime(2);

                            Unit = new Unit(unitId, name, createdDate);
                        }
                    }
                }
            }

            return Unit;
        } // Feito

        public bool Add(Unit entity, User adminUser)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO [dbo].[unit] (name, created_date)
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

        public bool Update(Unit entity, User adminUser)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE [dbo].[unit] SET name = @name WHERE id = @id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("@id", entity.Id);
                    cmd.Parameters.AddWithValue("@name", entity.Name);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        } // Feito

        public bool Delete(int id, User adminUser)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"DELETE [dbo].[unit] WHERE id = @id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("@id", id);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        } // Feito

    }
}
