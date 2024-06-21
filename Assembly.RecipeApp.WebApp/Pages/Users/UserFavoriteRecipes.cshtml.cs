using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assembly.RecipeApp.WebApp.Pages.Users
{
    public class UserFavoriteRecipesModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IRecipeService _recipeService;

        public User User { get; private set; }

        [BindProperty]
        public string UserImage { get; set; }

        public UserFavoriteRecipesModel(IUserService userService, IRecipeService recipeService)
        {
            _userService = userService;
            _recipeService = recipeService;
        }

        public List<Recipe> FavoriteRecipes { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; } = 8; // Adjust as needed
        public int PreviousPage => CurrentPage > 1 ? CurrentPage - 1 : 1;
        public int NextPage => CurrentPage < TotalPages ? CurrentPage + 1 : TotalPages;

        public IActionResult OnGet(int? selectedPage)
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

            // Check if User is Admin
            HttpContext.Session.SetString("IsAdmin", User.IsAdmin ? "true" : "false");

            // Set userImage property
            UserImage = string.IsNullOrEmpty(User.ImageSource) ? "/images/b750f1dc-0625-4022-9daa-7c9b1f377fdc_default-image.jpg.png" : User.ImageSource.ToString();

            // Get all Recipes from the service
            var userFavoriteRecipes = _recipeService.GetUserFavoriteRecipes(User.Id);
            User.UserFavoriteRecipes = userFavoriteRecipes; 

            // Pagination
            TotalPages = (int)Math.Ceiling((double)userFavoriteRecipes.Count / PageSize);
            CurrentPage = selectedPage ?? 1;

            // Get Recipes for the current page
            FavoriteRecipes = userFavoriteRecipes.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            return Page();
        }

        public IActionResult OnPostUnfavorite(int recipeId, int userId, int currentPage)
        {
            OnGet(recipeId);

            _recipeService.RemoveFavorite(recipeId, userId);

            OnGet(recipeId);

            // Redirect to the same page after deletion
            return RedirectToPage(new { selectedPage = currentPage });
        }

    }
}
