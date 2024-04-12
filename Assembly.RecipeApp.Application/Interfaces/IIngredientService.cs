using Assembly.RecipeApp.Domain.Model;

namespace Assembly.RecipeApp.Application.Interfaces
{
    public interface IIngredientService
    {
        List<Ingredient> GetAll();
        Ingredient GetById(int id);
        bool Add(Ingredient entity, User adminUser);
        bool Update(Ingredient entity, User adminUser);
        bool Delete(int id, User adminUser);

        List<Ingredient> GetRecipeIngredients(int recipeId);
    }

}
