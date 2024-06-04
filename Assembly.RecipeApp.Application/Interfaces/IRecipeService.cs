using Assembly.RecipeApp.Domain.Model;

namespace Assembly.RecipeApp.Application.Interfaces
{
    public interface IRecipeService : IService<Recipe>
    {
        List<Recipe> GetByUserId(int userId);
        Recipe GetByTitle(string title);
        List<Recipe> GetFilteredRecipes(string name);
        List<Recipe> GetFilteredRecipesPaged(string searchTerm, int currentPage, int pageSize);
        List<Recipe> GetTopRatedRecipes(int count);
        List<Recipe> GetAllApproved();
        List<Recipe> GetApprovedPaged(int currentPage, int pageSize);
        List<Recipe> GetAllBlocked();
    }

}
