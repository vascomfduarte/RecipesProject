using Assembly.RecipeApp.Domain.Model;

namespace Assembly.RecipeApp.Application.Interface
{
    public interface IRecipeService : IService<Recipe>
    {
        List<Recipe> GetFilteredRecipes(string name);
    }

}
