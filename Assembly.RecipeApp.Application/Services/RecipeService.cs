using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository.Interfaces;
using Assembly.RecipeApp.Repository.Repos;
using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;

namespace Assembly.RecipeApp.Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IPreparationMethodRepository _preparationMethodRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IIngredientRepository _ingredientRepository;

        public RecipeService(IRecipeRepository recipeRepository, 
                              IPreparationMethodRepository preparationMethodRepository,
                              ICommentRepository commentRepository, 
                              IIngredientRepository ingredientRepository)
        {
            _recipeRepository = recipeRepository;
            _preparationMethodRepository = preparationMethodRepository;
            _ingredientRepository = ingredientRepository;
            _commentRepository = commentRepository;
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
            //string recipeString = $"{recipe.Title}|{recipe.Instructions}|{recipe.ImageSource}|{recipe.MinutesToCook}|{(recipe.IsApproved ? "1" : "0")}|{recipe.UserId}|{recipe.DifficultyId}|{recipe.CreatedAt:yyyy-MM-dd}";

            // Call the UserRepository's Add method with the formatted string representation of the User
            //return _recipeRepository.Add(recipeString);

            throw new NotImplementedException();

        }  // Para eliminar

        public List<Recipe> GetAll()
        {
            return _recipeRepository.GetAll();
        } // Feito 

        public Recipe GetById(int recipeId)
        {
            Recipe r = _recipeRepository.GetById(recipeId);
            //PreparationMethod p = _preparationMethodRepository.GetByRecipeId(recipeId);
            //List<Ingredient> i = _ingredientRepository.GetByRecipeId(recipeId);
            //List<Comment> c = _commentRepository.GetByRecipeId(recipeId);

            return r;
            //return new Recipe(r.Id, r.Title, r.Description, p, r.ImageSource, r.MinutesToCook, r.IsApproved, r.User, r.Difficulty, r.Ratings, r.Categories, i, c, r.CreatedBy, r.CreatedDate);
        } // Feito 

        public List<Recipe> GetFilteredRecipes(string name)
        { 
            return _recipeRepository.GetFilteredRecipes(name);
        }

        public List<Recipe> GetTopRatedRecipes(int count)
        {
            return _recipeRepository.GetTopRatedRecipes(count);

            //throw new NotImplementedException();
        }

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
