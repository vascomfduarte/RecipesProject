using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;
using Services;

namespace WebApp.Pages
{
    public class SearchResultsModel : PageModel
    {
        public List<Recipe> SearchResults { get; private set; }
        //public List<User> UserResults { get; set; }

        public string SearchTerm;

        public int ResultsCounter = 0;

        private static RecipeServices _recipeServices = new RecipeServices();
        private static UserServices _userServices = new UserServices();

        public IActionResult OnGet(string query)
        {
            SearchTerm = query;
            if (string.IsNullOrEmpty(query))
                query = "0";

            SearchResults = _recipeServices.GetRecipesByName(query);

            //UserResults = _userServices.GetUserById(int.Parse(query));

            ResultsCounter = SearchResults?.Count ?? 0;

            return Page();
        }
    }
}
