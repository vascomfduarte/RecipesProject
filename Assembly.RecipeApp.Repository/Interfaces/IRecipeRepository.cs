using Assembly.RecipeApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.RecipeApp.Repository.Interfaces
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        bool AddFavorite(int recipeId, int userId);
        bool RemoveFavorite(int recipeId, int userId);
        List<Recipe> GetByUserId(int userId);
        List<Recipe> GetUserFavoriteRecipes(int userId);
        Recipe GetByTitle(string title);
        List<Recipe> GetFilteredRecipes(string name);
        List<Recipe> GetFilteredRecipesPaged(string searchTerm, int currentPage, int pageSize);
        List<Recipe> GetTopRatedRecipes(int count);
        List<Recipe> GetApprovedPaged(int currentPage, int pageSize);

    }
}
