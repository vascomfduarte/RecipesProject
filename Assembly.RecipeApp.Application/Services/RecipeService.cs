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
            if (_recipeRepository.GetAll().Any(r => r.Title == recipe.Title))
                throw new ArgumentException("Title is already in use.", nameof(recipe.Title));

            // Set isApproved to false by default if not provided
            recipe.SetIsApproved(recipe, false);

            // Call the RecipeRepository's Add method
            return _recipeRepository.Add(recipe);
        } // Feito 

        public List<Recipe> GetAll()
        {
            return _recipeRepository.GetAll();
        } // Feito 

        public List<Recipe> GetAllBlocked()
        {
            List<Recipe> recipes = new List<Recipe>();

            foreach (Recipe recipe in _recipeRepository.GetAll())
            {
                if (recipe.IsApproved is false)
                {
                    recipes.Add(recipe);
                }
            }

            return recipes;
        } // Feito 

        public List<Recipe> GetAllApproved()
        {
            List<Recipe> recipes = new List<Recipe>();

            foreach (Recipe recipe in _recipeRepository.GetAll())
            {
                if (recipe.IsApproved is true)
                {
                    recipes.Add(recipe);
                }
            }

            return recipes;
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

        public List<Recipe> GetByUserId(int userId)
        {
            return _recipeRepository.GetByUserId(userId);
        } // Feito 

        public List<Recipe> GetFilteredRecipes(string name)
        {
            List<Recipe> recipes = new List<Recipe>();

            foreach (Recipe recipe in _recipeRepository.GetFilteredRecipes(name))
            {
                if (recipe.IsApproved == true)
                {
                    recipes.Add(recipe);
                }
            }

            return recipes;
        } // Feito

        public List<Recipe> GetTopRatedRecipes(int count)
        {
            List<Recipe> recipes = new List<Recipe>();

            foreach (Recipe recipe in _recipeRepository.GetTopRatedRecipes(count))
            {
                if (recipe.IsApproved == true)
                {
                    recipes.Add(recipe);
                }
            }

            return recipes;
        } // Feito

        public bool Update(Recipe entity)
        {
            return _recipeRepository.Update(entity);
        } // Feito

        public bool Delete(Recipe recipe)
        {
            return _recipeRepository.Delete(recipe);
        } // Feito

    }
}
