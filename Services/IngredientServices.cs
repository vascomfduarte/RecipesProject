using Model;
using Repository;

namespace Services
{
    public class IngredientServices
    {
        private static IngredientRepository _ingredientRepository = new IngredientRepository();

        internal List<string> GetIngredient(int recipeId)
        {
            throw new NotImplementedException();
        }
    }
}