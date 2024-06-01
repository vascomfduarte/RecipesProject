using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assembly.RecipeApp.WebApp.Pages.Users
{
    public class CreateUserRecipesPage2Model : PageModel
    {
        private readonly IUserService _userService;
        private readonly IRecipeService _recipeService;
        private readonly IDifficultyService _difficultyService;
        private readonly IWebHostEnvironment _hostingEnvironment;
                      

        public Recipe Recipe { get; set; }
        public User User { get; private set; }


        public List<Difficulty> Difficulties { get; set; }


        [BindProperty]
        public string UserImage { get; set; }

        public CreateUserRecipesPage2Model(IUserService userService, IRecipeService recipeService, IDifficultyService difficultyService, IWebHostEnvironment hostingEnvironment)
        {
            _userService = userService;
            _recipeService = recipeService;
            _difficultyService = difficultyService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult OnGet()
        {
            Difficulties = _difficultyService.GetAll();

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

            // Set Image properties
            UserImage = string.IsNullOrEmpty(User.ImageSource) ? "/images/b750f1dc-0625-4022-9daa-7c9b1f377fdc_default-image.jpg.png" : User.ImageSource.ToString();

            return Page();
        }

        public IActionResult OnPost()
        {
            OnGet();

            return Page();

        }

    }
}

