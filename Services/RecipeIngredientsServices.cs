using Model;
using Repository;

namespace Services
{
    public class RecipeIngredientsServices
    {
        private static IngredientRepository _ingredientRepository = new IngredientRepository();
        private static UnitRepository _unitRepository = new UnitRepository();
        // Tem de aceder aos serviços. Tenho de alterar

        public List<RecipeIngredients> GetRecipeIngredients(int recipeId)
        {
            // Retrieve the list of recipe strings from the repository
            List<string> ingredientStrings = _ingredientRepository.GetIngredient(recipeId);
            List<string> unitStrings = _unitRepository.GetIngredient(recipeId);



            throw new NotImplementedException();
        }

    }
}