using Assembly.Recipe.Domain.Model;
using Repository;

namespace Assembly.RecipeApp.Application.Services
{
    public class RecipeIngredientsServices
    {
        private static IngredientServices _ingredientServices = new IngredientServices();
        private static UnitServices _unitServices = new UnitServices();

        public List<RecipeIngredients> GetRecipeIngredients(int recipeId)
        {
            // Retrieve the list of recipe strings from the repository
            List<string> ingredientStrings = _ingredientServices.GetIngredient(recipeId);
            List<string> unitStrings = _unitServices.GetIngredient(recipeId);

            throw new NotImplementedException();
        }

    }
}