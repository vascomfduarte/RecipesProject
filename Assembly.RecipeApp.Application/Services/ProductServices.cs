using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository;
using Assembly.RecipeApp.Repository.Interfaces;
using Assembly.RecipeApp.Repository.Repos;

namespace Assembly.RecipeApp.Application.Services
{
    public class ProductServices : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductServices(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        } // Feito 

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        } // Feito 

        public bool Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
