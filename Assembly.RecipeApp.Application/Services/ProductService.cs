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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
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

        public bool Add(User currentUser, Product entity)
        {
            if (currentUser.IsAdmin)
            {
                // Validate if Ingredient with the same name already exists
                if (GetAll().Any(i => i.Name == entity.Name))
                    throw new ArgumentException("A product with the same name already exists.", nameof(entity.Name));

                _productRepository.Add(entity);
                return true;
            }

            return false;
        } // Feito 

        public bool Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(User currentUser, Product entity)
        {
            if (currentUser.IsAdmin)
            {
                _productRepository.Delete(entity);
                return true;
            }

            return false;
        } // Feito 

        public bool Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Product entity)
        {
            return _productRepository.Update(entity);
        }


    }
}
