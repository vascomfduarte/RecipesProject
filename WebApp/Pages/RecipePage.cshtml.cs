using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;
using Services;

namespace WebApp.Pages
{
    public class RecipePageModel : PageModel
    {
        public List<string> Recipes { get; set; }

        private static RecipeServices _recipeServices = new RecipeServices();

        public void OnGet()
        {
            //Recipes = new List<Recipe>() { };

            //Recipes = _recipeServices.GetRecipes();
        }
    }
}
