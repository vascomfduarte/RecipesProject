using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Application.Services;
using Assembly.RecipeApp.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assembly.RecipeApp.WebApp.Pages.Users
{
    public class ManageRecipesModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IRecipeService _recipeService;

        public User User { get; private set; }

        [BindProperty]
        public string UserImage { get; set; }

        public ManageRecipesModel(IUserService userService, IRecipeService recipeService)
        {
            _userService = userService;
            _recipeService = recipeService;
        }

        public List<Recipe> Recipes { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; } = 8; // Adjust as needed
        public int PreviousPage => CurrentPage > 1 ? CurrentPage - 1 : 1;
        public int NextPage => CurrentPage < TotalPages ? CurrentPage + 1 : TotalPages;

        public IActionResult OnGet(int? page)
        {
            // Retrieve UserId from session
            var userId = HttpContext.Session.GetInt32("Id");

            // Handle case where user is not logged in
            if (userId is null)
            {
                return RedirectToPage("/Users/Login");
            }

            // Fetch the user by id
            User = _userService.GetById(userId.Value);

            // Handle case where user is not logged in
            if (User is null)
            {
                return RedirectToPage("/Users/Login");
            }

            if (User.IsAdmin is false)
            {
                return RedirectToPage("/Index");
            }

            // Set userImage property
            UserImage = string.IsNullOrEmpty(User.ImageSource) ? "/images/b750f1dc-0625-4022-9daa-7c9b1f377fdc_default-image.jpg.png" : User.ImageSource.ToString();


            // Get all recipes from the service
            var allRecipes = _recipeService.GetAll();

            // Pagination
            TotalPages = (int)Math.Ceiling((double)allRecipes.Count / PageSize);
            CurrentPage = page ?? 1;

            // Get recipes for the current page
            Recipes = allRecipes.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            return Page();
        }
    }
}
