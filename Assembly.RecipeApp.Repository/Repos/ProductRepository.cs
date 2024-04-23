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
        }

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public Product Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public Product Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Product Update(Product entity)
        {
            throw new NotImplementedException();
        }

        bool IRepository<Product>.Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
