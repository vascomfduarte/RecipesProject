using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assembly.RecipeApp.WebApp.Pages.Users
{
    public class AccountModel : PageModel
    {
        private readonly ILogger<AccountModel> _logger;
        private readonly IUserService _userService;

        public User User { get; private set; }

        [BindProperty]
        public string UserImage { get; set; }

        public AccountModel(ILogger<AccountModel> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult OnGet()
        {
            // Retrieve UserId from session
            var userId = HttpContext.Session.GetInt32("Id");

            if (userId is null)
            {
                // Handle case where user is not logged in
                return RedirectToPage("/Users/Login");
            }

            // Fetch the user by id
            User = _userService.GetById(userId.Value);

            // If the user is null, you might want to handle this case
            if (User == null)
            {
                // Handle case where user is not found
                return RedirectToPage("/Index");
            }

            // Set userImage property
            UserImage = string.IsNullOrEmpty(User.ImageSource) ? "https://i.imgur.com/qlEw2Rz.jpeg" : User.ImageSource.ToString();

            return Page();
        }
    }
}
