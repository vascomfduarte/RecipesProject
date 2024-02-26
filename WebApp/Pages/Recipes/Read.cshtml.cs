using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;
using Services;

namespace WebApp.Pages.Recipes
{
    public class ReadModel : PageModel
    {
        public List<Recipe> Recipes { get; set; }

        private static RecipeServices _recipeServices = new RecipeServices();

        public void OnGet(int recipeId)
        {
            Recipes = _recipeServices.GetRecipesById(recipeId);
        }
    }
}
