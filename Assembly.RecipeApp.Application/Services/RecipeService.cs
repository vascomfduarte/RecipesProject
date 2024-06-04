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
        private readonly IRatingRepository _ratingRepository;

        public RecipeService(IRecipeRepository recipeRepository, 
                              IPreparationMethodRepository preparationMethodRepository,
                              ICommentRepository commentRepository, 
                              IIngredientRepository ingredientRepository,
                              IRatingRepository ratingRepository)
        {
            _recipeRepository = recipeRepository;
            _preparationMethodRepository = preparationMethodRepository;
            _ingredientRepository = ingredientRepository;
            _commentRepository = commentRepository;
            _ratingRepository = ratingRepository;
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

        public List<Recipe> GetApprovedPaged(int currentPage, int pageSize)
        {
            List<Recipe> recipes = new List<Recipe>();

            foreach (Recipe recipe in _recipeRepository.GetApprovedPaged(currentPage, pageSize))
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

        public Recipe GetByTitle(string title)
        {
            Recipe r = _recipeRepository.GetByTitle(title);
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

        public List<Recipe> GetFilteredRecipesPaged(string searchTerm, int currentPage, int pageSize)
        {
            List<Recipe> recipes = new List<Recipe>();

            foreach (Recipe recipe in _recipeRepository.GetFilteredRecipesPaged(searchTerm, currentPage, pageSize))
            {
                if (recipe.IsApproved is true)
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
            // Delete Recipe associated comments
            if (_commentRepository.GetByRecipeId(recipe.Id).Any())
            {
                _commentRepository.DeleteByRecipeId(recipe.Id);
            }

            // Delete Recipe associated ratings
            if (_ratingRepository.GetByRecipeId(recipe.Id).Any())
            {
                _ratingRepository.DeleteByRecipeId(recipe.Id);
            }


            // Delete Recipe ingredients
            if (_ingredientRepository.GetRecipeIngredients(recipe.Id).Any())
            {
                _ingredientRepository.DeleteByRecipeId(recipe.Id);
            }

            // Delete Recipe's preparation method
            var preparationMethod = _preparationMethodRepository.GetByRecipeId(recipe.Id);
            if (preparationMethod != null && preparationMethod.Steps != null && preparationMethod.Steps.Any())
            {
                _preparationMethodRepository.DeleteByRecipeId(recipe.Id);
            }

            return _recipeRepository.Delete(recipe);
        } // Feito

    }
}
