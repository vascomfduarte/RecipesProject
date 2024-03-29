using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assembly.RecipeApp.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IRecipeService _recipeService;

        public IEnumerable<Recipe> Recipes { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, IRecipeService recipeServices)
        {
            _logger = logger;
            _recipeService = recipeServices;
        }

        public void OnGet()
        {
            Recipes = _recipeService.GetAll();
        }
    }
}
