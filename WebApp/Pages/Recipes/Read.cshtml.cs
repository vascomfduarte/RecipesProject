using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;
using Services;

namespace WebApp.Pages.Recipes
{
    public class ReadModel : PageModel
    {
        public Recipe Recipe { get; set; }

        private static RecipeServices _recipeServices = new RecipeServices();

        public void OnGet(int recipeId)
        {
            Recipe = _recipeServices.GetRecipeById(recipeId);        
        }
    }
}
