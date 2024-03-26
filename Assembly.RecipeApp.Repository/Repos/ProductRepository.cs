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
            throw new NotImplementedException();
        }

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Product Add(Product entity)
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
    }
}
