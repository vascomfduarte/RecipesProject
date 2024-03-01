using Assembly.RecipeApp.Application.Interface;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository;

namespace Assembly.RecipeApp.Application.Services
{
    internal class UnitServices : IRecipeService
    {
        private static UnitRepository _unitRepository = new UnitRepository();

        public bool Add(Recipe entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Recipe> GetAll()
        {
            throw new NotImplementedException();
        }

        public Recipe GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Recipe> GetFilteredProducts(string name)
        {
            throw new NotImplementedException();
        }

        public bool Update(Recipe entity)
        {
            throw new NotImplementedException();
        }

        internal List<string> GetIngredient(int recipeId)
        {
            throw new NotImplementedException();
        }
    }
}
