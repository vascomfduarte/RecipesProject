using Assembly.RecipeApp.Domain.Model;
using System.ComponentModel;

namespace Assembly.RecipeApp.Application.Interfaces
{
    public interface IProductService : IService<Product>
    {
        bool Add(User currentUser, Product entity);
        public bool Delete(User currentUser, Product entity);
    }
}