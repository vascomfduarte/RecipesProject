using Assembly.RecipeApp.Domain.Model;

namespace Assembly.RecipeApp.Application.Interface
{
    public interface IIngredientService : IService<Ingredient>
    {
        bool Add(Ingredient entity, User user);

        List<Ingredient> GetRecipeIngredients(int recipeId);
    }

}
