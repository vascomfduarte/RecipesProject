using Assembly.RecipeApp.Application.Services;
using Assembly.RecipeApp.Domain.Model;

namespace Assembly.RecipeApp.Application.Interfaces
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        Category GetById(int id);
        bool Add(Category entity, User adminUser);
        bool AddRecipe(int categoryId, int recipeId);
        bool RemoveRecipe(int categoryId, int recipeId);
        bool Update(Category entity, User adminUser);
        bool Delete(int id, User adminUser);
    }
}