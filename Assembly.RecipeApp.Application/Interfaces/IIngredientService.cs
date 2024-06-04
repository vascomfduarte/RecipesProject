using Assembly.RecipeApp.Domain.Model;

namespace Assembly.RecipeApp.Application.Interfaces
{
    public interface IIngredientService
    {
        List<Ingredient> GetAll();
        Ingredient GetById(int id);
        bool Add(Ingredient ingredient, int recipeId);
        bool Update(Ingredient entity, User adminUser);
        bool Delete(Ingredient entity);
        List<Ingredient> GetRecipeIngredients(int recipeId);
    }

}
