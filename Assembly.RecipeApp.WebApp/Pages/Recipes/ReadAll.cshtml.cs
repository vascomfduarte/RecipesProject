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

        public IEnumerable<Recipe> Recipes { get; private set; }
        public string SearchTerm { get; set; }

        public int ResultsCounter = 0;

        public ReadAllModel(ILogger<ReadAllModel> logger, IRecipeService recipeServices)
        {
            _logger = logger;
            _recipeService = recipeServices;
        }

        public IActionResult OnGet(string query)
        {
            SearchTerm = query;
            //Recipes = _recipeService.GetAll();
            Recipes = _recipeService.GetFilteredRecipes(query);

            ResultsCounter = Recipes.Count();

            return Page();
        }

    }
}
