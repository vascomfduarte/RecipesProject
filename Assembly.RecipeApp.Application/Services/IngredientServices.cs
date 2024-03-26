using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository.Interfaces;
using Assembly.RecipeApp.Repository.Repos;
using System.Globalization;

namespace Assembly.RecipeApp.Application.Services
{
    public class IngredientServices : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientServices(IIngredientRepository ingredientRepository)
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
        }

        public bool Add(Ingredient ingredient, User currentUser)
        {
            if (currentUser.IsAdmin)
            {
                // Validate if Ingredient with the same name already exists
                if (GetAll().Any(i => i.Name == ingredient.Name))
                    throw new ArgumentException("An ingredient with the same name already exists.", nameof(ingredient.Name));

                _ingredientRepository.Add(ingredient);
                return true;
            }

            return false;
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
