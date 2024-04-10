using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assembly.RecipeApp.WebApp.Pages.Recipes
{
    public class GetModel : PageModel
    {
        private readonly ILogger<GetModel> _logger;
        private readonly IRecipeService _recipeService;
        private readonly IRatingService _ratingService;

        public Recipe Recipe { get; private set; }

        public int RatingsCounter { get; private set; }
        public int Rating { get; private set; }
        public int RatingCount { get; private set; }


        public GetModel(ILogger<GetModel> logger, IRecipeService recipeServices, IRatingService ratingService)
        {
            _logger = logger;
            _recipeService = recipeServices;
            _ratingService = ratingService;
        }

        public IActionResult OnGet(int id)
        {           
            Recipe = _recipeService.GetById(id);

            if (Recipe.GetRecipeRating() > 0)
            {
                Rating = Recipe.GetRecipeRating();
                RatingCount = Recipe.GetRecipeRatingCount();
            }
            else
            {
                Rating = 0;
                RatingCount = 0;
            }


            //Rating = Recipe.GetRecipeRating();
            //RatingCount = Recipe.GetRecipeRatingCount();

            //Rating = 3;
            //RatingCount = 26765;

            return Page();
        }
    }
}
