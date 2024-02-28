using Repository;

namespace Assembly.Recipe.Application.Services
{
    internal class UnitServices
    {
        private static UnitRepository _unitRepository = new UnitRepository();

        internal List<string> GetIngredient(int recipeId)
        {
            throw new NotImplementedException();
        }
    }
}