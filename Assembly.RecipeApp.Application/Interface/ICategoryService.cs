using Assembly.RecipeApp.Application.Services;
using Assembly.RecipeApp.Domain.Model;

namespace Assembly.RecipeApp.Application.Interface
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        Category GetById(int id);
        bool Add(Category entity, User adminUser);
        bool Update(Category entity, User adminUser);
        bool Delete(int id, User adminUser);
    }
}