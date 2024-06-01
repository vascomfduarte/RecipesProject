using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Application.Services;
using Assembly.RecipeApp.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assembly.RecipeApp.WebApp.Pages.Admin
{
    public class CreateDifficultyModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IDifficultyService _difficultyService;

        public User User { get; private set; }

        [BindProperty]
        public string UserImage { get; set; }


        public Difficulty Difficulty { get; set; }

        [BindProperty]
        public string Name { get; set; }


        public CreateDifficultyModel(IUserService userService, IDifficultyService difficultyService)
        {
            _userService = userService;
            _difficultyService = difficultyService;
        }

        public IActionResult OnGet()
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

            return Page();
        }

        public IActionResult OnPost()
        {
            OnGet();

            Difficulty difficulty = new Difficulty(name: Name);

            _difficultyService.Add(difficulty, User);            

            return RedirectToPage("/Admin/ManageDifficulties");

        }
    }
}

