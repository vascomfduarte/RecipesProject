using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Application.Services;
using Assembly.RecipeApp.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assembly.RecipeApp.WebApp.Pages.Admin
{
    public class ManageDifficultiesModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IDifficultyService _difficultyService;

        public User User { get; private set; }

        [BindProperty]
        public string UserImage { get; set; }

        public ManageDifficultiesModel(IUserService userService, IDifficultyService difficultyService)
        {
            _userService = userService;
            _difficultyService = difficultyService;
        }


        public List<Difficulty> Difficulties { get; set; }
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

            if (User.IsAdmin is false)
            {
                return RedirectToPage("/Index");
            }

            // Set userImage property
            UserImage = string.IsNullOrEmpty(User.ImageSource) ? "/images/b750f1dc-0625-4022-9daa-7c9b1f377fdc_default-image.jpg.png" : User.ImageSource.ToString();

            // Get all recipes from the service
            var allDifficulties = _difficultyService.GetAll();

            // Pagination
            TotalPages = (int)Math.Ceiling((double)allDifficulties.Count / PageSize);
            CurrentPage = selectedPage ?? 1;

            // Get recipes for the current page
            Difficulties = allDifficulties.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            return Page();
        }

        public IActionResult OnPostDeleteDifficulty(int difficultyId, int currentPage)
        {
            OnGet(CurrentPage);

            var difficulty = _difficultyService.GetById(difficultyId);
            if (difficulty != null)
            {
                _difficultyService.Delete(difficulty, User);
            }

            // Redirect to the same page after deletion
            return RedirectToPage(new { selectedPage = currentPage });
        }
    }
}
