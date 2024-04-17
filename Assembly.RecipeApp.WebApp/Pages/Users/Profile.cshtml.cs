using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.WebApp.Pages.Recipes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assembly.RecipeApp.WebApp.Pages.Users
{
    public class ProfileModel : PageModel
    {
        private readonly ILogger<GetAllModel> _logger;
        private readonly IUserService _userService;

        public User User { get; private set; }
        public int UserId { get; private set; }

        public string userImage;

        public ProfileModel(ILogger<GetAllModel> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult OnGet(int id)
        {
            id = 1;

            UserId = id;

            // Fetch the user by id
            User = _userService.GetById(id);

            // If the user is null, you might want to handle this case
            if (User == null)
            {
                // Handle case where user is not found
                return NotFound();
            }

            // Set userImage property
            userImage = User.ImageSource is null ? "https://n9.cl/yuh9ik" : User.ImageSource.ToString();

            return Page();
        }
    }
}
