using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Application.Services;
using Assembly.RecipeApp.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace Assembly.RecipeApp.WebApp.Pages.Recipes
{
    public class ReadAllModel : PageModel
    {
        private readonly ILogger<ReadAllModel> _logger;
        private readonly IRecipeService _recipeService;
        private readonly IRatingService _ratingService;

        public IEnumerable<Recipe> Recipes { get; private set; }
        public string SearchTerm { get; set; }

        public int recipesCounter = 0;
        public int ratingsCounter = 0;
        public double totalRating = 0;

        public ReadAllModel(ILogger<ReadAllModel> logger, IRecipeService recipeServices, IRatingService ratingService)
        {
            _logger = logger;
            _recipeService = recipeServices;
            _ratingService = ratingService;
        }

        public IActionResult OnGet(string query)
        {
            SearchTerm = query;

            Recipes = query is null ? _recipeService.GetAll() : _recipeService.GetFilteredRecipes(query);

            recipesCounter = Recipes.Count();

            return Page();
        }

    }
}
