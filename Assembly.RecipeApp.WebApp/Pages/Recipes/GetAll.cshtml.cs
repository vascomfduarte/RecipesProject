using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Application.Services;
using Assembly.RecipeApp.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace Assembly.RecipeApp.WebApp.Pages.Recipes
{
    public class GetAllModel : PageModel
    {
        private readonly ILogger<GetAllModel> _logger;
        private readonly IRecipeService _recipeService;
        private readonly IRatingService _ratingService;

        public List<Recipe> Recipes { get; private set; }
        public string SearchTerm { get; set; }

        public int recipesCounter = 0;

        private const int PageSize = 8; // Number of Recipes per page
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PreviousPage => CurrentPage > 1 ? CurrentPage - 1 : 1;
        public int NextPage => CurrentPage < TotalPages ? CurrentPage + 1 : TotalPages;

        public GetAllModel(ILogger<GetAllModel> logger, IRecipeService recipeServices, IRatingService ratingService)
        {
            _logger = logger;
            _recipeService = recipeServices;
            _ratingService = ratingService;
        }

        public IActionResult OnGet(string query, int? selectedPage)
        {
            SearchTerm = query;

            Recipes = query is null ? _recipeService.GetAllApproved() : _recipeService.GetFilteredRecipes(query);

            recipesCounter = Recipes.Count();

            // Pagination
            TotalPages = (int)Math.Ceiling((double)recipesCounter / PageSize);
            CurrentPage = Math.Max(1, Math.Min(selectedPage ?? 1, TotalPages));

            // Fetch users for the current page
            Recipes = query is null ? _recipeService.GetApprovedPaged(CurrentPage, PageSize) : _recipeService.GetFilteredRecipesPaged(query, CurrentPage, PageSize);

            return Page();
        }

    }
}
