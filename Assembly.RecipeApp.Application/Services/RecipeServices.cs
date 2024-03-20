using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository.Interfaces;
using Assembly.RecipeApp.Repository.Repos;
using System.ComponentModel;
using System.Globalization;

namespace Assembly.RecipeApp.Application.Services
{
    public class RecipeServices : IRecipeService
    {
        IRecipeRepository _recipeRepository;

        public RecipeServices(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public bool Add(Recipe recipe)
        {
            // Validate if Title is already in use
            if (GetAll().Any(r => r.Title == recipe.Title))
                throw new ArgumentException("Title is already in use.", nameof(recipe.Title));

            // Validate image source format
            if (recipe.ImageSource is not null)
            {
                if (!Uri.TryCreate(recipe.ImageSource, UriKind.Absolute, out Uri uriResult) || uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps)
                {
                    recipe.ImageSource = "https://iili.io/JGnl0eS.png";
                    //throw new ArgumentException("Invalid image URL.", nameof(user.ImageSource));
                }
            }
            else if (recipe.ImageSource is null)
            {
                // Set a default to ImageSource
                recipe.ImageSource = "https://iili.io/JGnl0eS.png";
            }

            // Set isBlocked to false by default if not provided
            recipe.SetIsApproved(recipe, false);

            // Format the User object's properties into a string representation
            string recipeString = $"{recipe.Title}|{recipe.Instructions}|{recipe.ImageSource}|{recipe.MinutesToCook}|{(recipe.IsApproved ? "1" : "0")}|{recipe.UserId}|{recipe.DifficultyId}|{recipe.CreatedAt:yyyy-MM-dd}";

            // Call the UserRepository's Add method with the formatted string representation of the User
            return _recipeRepository.Add(recipeString);
        }  // Para eliminar

        public List<Recipe> GetAll()
        {
            return _recipeRepository.GetAll();
        } // Feito 

        public Recipe GetById(int id)
        {
            return _recipeRepository.GetById(id);
        } // Feito 

        public List<Recipe> GetFilteredRecipes(string name)
        { 
            return _recipeRepository.GetFilteredRecipes(name);
        } // Feito 

        public bool Update(Recipe entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
