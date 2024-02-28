using Assembly.Recipe.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;

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
