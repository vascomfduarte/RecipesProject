using Assembly.RecipeApp.Domain.Model;

namespace Assembly.RecipeApp.Application.Interfaces
{
    public interface IRecipeService : IService<Recipe>
    {
        List<Recipe> GetByUserId(int userId);
        List<Recipe> GetFilteredRecipes(string name);
        List<Recipe> GetTopRatedRecipes(int count);
        List<Recipe> GetAllApproved();
        List<Recipe> GetAllBlocked();
    }

}
