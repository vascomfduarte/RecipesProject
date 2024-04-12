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

        public bool Add(Product entity)
        {
            //if (currentUser.IsAdmin)
            //{
            //    // Validate if Ingredient with the same name already exists
            //    if (GetAll().Any(i => i.Name == ingredient.Name))
            //        throw new ArgumentException("An ingredient with the same name already exists.", nameof(ingredient.Name));

            //    _ingredientRepository.Add(ingredient);
            //    return true;
            //}

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
