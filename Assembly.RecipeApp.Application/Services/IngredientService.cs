using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository.Interfaces;
using Assembly.RecipeApp.Repository.Repos;
using System.Globalization;

namespace Assembly.RecipeApp.Application.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientService(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public List<Ingredient> GetAll()
        { 
            return _ingredientRepository.GetAll();
        } // Feito 

        public Ingredient GetById(int id)
        {
            return _ingredientRepository.GetById(id);
        } // Feito 

        public List<Ingredient> GetRecipeIngredients(int recipeId)
        {
            return _ingredientRepository.GetRecipeIngredients(recipeId);
        } // Feito

        public bool Add(Ingredient ingredient, User currentUser)
        {
            throw new NotImplementedException();
        }

        public bool Update(Ingredient entity, User adminUser)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id, User adminUser)
        {
            throw new NotImplementedException();
        }

    }
}
