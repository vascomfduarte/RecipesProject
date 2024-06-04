using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.RecipeApp.Repository.Repos
{
    public class ProductRepository : IProductRepository
    {
        private static string _connectionString = ConnectionStringProvider.GetConnectionString();

        public Product Product;

        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();

            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM product;";

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

                            var product = new Product(id, name, createdDate);

                            products.Add(product);
                        }
                    }
                }
            }

            return products;
        } // Feito 

        public Product GetById(int productId)
        {
            Product product = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM product WHERE id = @id;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", productId);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            DateTime createdDate = reader.GetDateTime(2);

                            product = new Product(id, name, createdDate);
                        }
                    }
                }
            }

            return product;
        } // Feito

        public bool Add(Product entity)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO [dbo].[product] (name, created_date)
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

        public bool Delete(Product entity)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"DELETE FROM [dbo].[product] WHERE id = @id";

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

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Product entity)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE [dbo].[product]
                               SET name = @name
                               WHERE id = @id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("@name", entity.Name);
                    cmd.Parameters.AddWithValue("@id", entity.Id);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        } // Feito

        Product IRepository<Product>.Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
