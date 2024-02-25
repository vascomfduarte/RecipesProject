using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository
    {
        private static string _connectionString = ConnectionStringProvider.GetConnectionString();

        public List<string> GetUserById(int userId)
        {
            List<string> returnUser = new List<string>();

            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query;

                if (userId == 0)
                    query = "SELECT * FROM [dbo].[user]";
                else
                    query = "SELECT * FROM [dbo].[user] WHERE id = @userId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;

                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string user = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}",
                                                          reader["ID"],
                                                          reader["username"],
                                                          reader["password"],
                                                          reader["email"],
                                                          reader["first_name"],
                                                          reader["last_name"],
                                                          reader["content_bio"],
                                                          reader["image_source"],
                                                          reader["is_admin"],
                                                          reader["is_blocked"],
                                                          reader["created_at"]);
                            returnUser.Add(user);
                        }
                    }
                }

            }

            return returnUser;
        }
    }
}

// Construct the string representation of the recipe
//string user = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}",
//                              reader["ID"],
//                              reader["username"],
//                              reader["password"],
//                              reader["email"],
//                              reader["first_name"],
//                              reader["last_name"],
//                              reader["content_bio"],
//                              reader["image_source"],
//                              reader["is_admin"],
//                              reader["is_blocked"],
//                              reader["created_at"]);
