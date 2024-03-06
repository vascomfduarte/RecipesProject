
using System.Data.SqlClient;
using System.Data;

namespace Assembly.RecipeApp.Repository
{
    public class UnitRepository
    {
        private static string _connectionString = ConnectionStringProvider.GetConnectionString();

        public string Unit;

        public bool Add(string unitString)
        {
            throw new NotImplementedException();
        }

        public string GetById(int unitId)
        {
            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM unit WHERE id = @id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameter to command
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = unitId;

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Construct the string representation of the recipe
                            Unit = string.Format("{0}|{1}",
                                                      reader["ID"],
                                                      reader["name"]);
                        }
                    }
                }
            }

            return Unit;
        }
    }
}
