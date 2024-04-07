using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assembly.RecipeApp.WebApp.Pages.Users
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        private readonly IUserService _userService;

        public IEnumerable<User> Users { get; private set; }
        public User User { get; private set; }

        public LoginModel(ILogger<LoginModel> logger, IUserService userServices)
        {
            _logger = logger;
            _userService = userServices;
        }

        public IActionResult OnGet(string username, string password)
        {
            //User = _userService.Login(username, password);

            return Page();
        }

    }
}
