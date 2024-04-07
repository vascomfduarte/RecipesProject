using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.WebApp.Pages.Recipes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Net.WebRequestMethods;

namespace Assembly.RecipeApp.WebApp.Pages.Users
{
    public class AccountSettingsModel : PageModel
    {
        private readonly ILogger<ReadAllModel> _logger;
        private readonly IUserService _userService;

        public User User { get; private set; }

        public string userImage;

        public AccountSettingsModel(ILogger<ReadAllModel> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult OnGet(int id)
        {
            id = 104;

            User = _userService.GetById(id);

            userImage = User.ImageSource is null ? "https://n9.cl/yuh9ik" : User.ImageSource.ToString();

            return Page();
        }
    }
}
