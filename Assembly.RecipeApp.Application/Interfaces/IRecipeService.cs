using Assembly.RecipeApp.Domain.Model;

namespace Assembly.RecipeApp.Application.Interfaces
{
    public interface IRecipeService : IService<Recipe>
    {
        bool AddFavorite(int recipeId, int userId);
        bool RemoveFavorite(int recipeId, int userId);
        List<Recipe> GetByUserId(int userId);
        List<Recipe> GetUserFavoriteRecipes(int userId);
        Recipe GetByTitle(string title);
        List<Recipe> GetFilteredRecipes(string name);
        List<Recipe> GetFilteredRecipesPaged(string searchTerm, int currentPage, int pageSize);
        List<Recipe> GetTopRatedRecipes(int count);
        List<Recipe> GetAllApproved();
        List<Recipe> GetApprovedPaged(int currentPage, int pageSize);
        List<Recipe> GetAllBlocked();        
        Recipe GetFeaturedRecipe();
    }

}
