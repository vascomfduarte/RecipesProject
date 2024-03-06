using Assembly.RecipeApp.Domain.Model;

namespace Assembly.RecipeApp.Application.Interfaces
{
    public interface IRecipeService : IService<Recipe>
    {
        List<Recipe> GetFilteredRecipes(string name);
    }

}
