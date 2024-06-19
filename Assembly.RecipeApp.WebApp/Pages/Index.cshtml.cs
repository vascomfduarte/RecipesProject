using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Application.Services;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.WebApp.Pages.Recipes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assembly.RecipeApp.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRecipeService _recipeService;
        private readonly IRatingService _ratingService;

        public IEnumerable<Recipe> Recipes { get; private set; }
        public IEnumerable<Recipe> TopRatedRecipes { get; private set; }
        public Recipe FeaturedRecipe { get; private set; }

        public IndexModel(IRecipeService recipeService, IRatingService ratingService)
        {
            _recipeService = recipeService;
            _ratingService = ratingService;
        }

        public IActionResult OnGet()
        {
            Recipes = _recipeService.GetAll();
            TopRatedRecipes = _recipeService.GetTopRatedRecipes(10);
            FeaturedRecipe = _recipeService.GetFeaturedRecipe();

            return Page();
        }
    }
}
