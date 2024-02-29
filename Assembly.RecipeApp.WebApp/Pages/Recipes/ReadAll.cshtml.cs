using Assembly.Recipe.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;

namespace WebApp.Pages.Recipes
{
    public class ReadAllModel : PageModel
    {
        public List<Recipe> Recipes { get; private set; }
        public string SearchTerm { get; set; }

        public int ResultsCounter = 0;

        private static RecipeServices _recipeServices = new RecipeServices();

        public IActionResult OnGet(string query)
        {
            SearchTerm = query;

            Recipes = _recipeServices.GetRecipesByName(query);

            ResultsCounter = Recipes?.Count ?? 0;

            return Page();
        }
    }
}
