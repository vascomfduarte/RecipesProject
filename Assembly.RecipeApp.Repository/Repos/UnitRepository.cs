
using System.Data.SqlClient;
using System.Data;
using Assembly.RecipeApp.Repository.Interfaces;
using Assembly.RecipeApp.Domain.Model;

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
            throw new NotImplementedException();
        }

        public bool Update(Unit entity, User adminUser)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id, User adminUser)
        {
            throw new NotImplementedException();
        }

    }
}
