using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;
using Services;

namespace WebApp.Pages
{
    public class SearchModel : PageModel
    {
        public List<Recipe> SearchResults { get; private set; }

        public string SearchTerm;

        public int ResultsCounter = 0;

        private static RecipeServices _recipeServices = new RecipeServices();

        public IActionResult OnGet(string query)
        {
            SearchTerm = query;
            if (string.IsNullOrEmpty(query))
                query = "";

            SearchResults = _recipeServices.GetRecipesByName(query);

            ResultsCounter = SearchResults?.Count ?? 0;

            return Page();
        }
    }
}
