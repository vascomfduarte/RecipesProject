using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Assembly.RecipeApp.WebApp.Pages.Users
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        private readonly IUserService _userService;

        [BindProperty]
        [Required(ErrorMessage = "Please enter your username.")]
        public string Username { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please enter your password.")]
        public string Password { get; set; }

        [TempData]
        public string UsernameErrorMessage { get; set; }

        [TempData]
        public string PasswordErrorMessage { get; set; }

        public LoginModel(ILogger<LoginModel> logger, IUserService userServices)
        {
            _logger = logger;
            _userService = userServices;
        }

        public void OnGet()
        {            
            UsernameErrorMessage = null;
        }

        public IActionResult OnPost() 
        {
            // Check if the username is provided
            if (string.IsNullOrEmpty(Username))
            {
                UsernameErrorMessage = "Please enter your username.";
                return Page();
            }

            // Check if the password is provided
            if (string.IsNullOrEmpty(Password))
            {
                PasswordErrorMessage = "Please enter your password.";
                return Page();
            }

            // Check if the user with the provided username exists
            User user = _userService.GetByUsername(Username);

            if (user == null)
            {
                // User with the provided username does not exist
                UsernameErrorMessage = "Invalid username or password.";
                return Page();
            }

            // Check if the user is blocked
            if (user.IsBlocked)
            {
                // User account is blocked
                PasswordErrorMessage = "Your account has been blocked.";
                return Page();
            }

            // User exists, now attempt to log in
            user = _userService.Login(Username, Password);

            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetInt32("Id", user.Id);
                return RedirectToPage("/Index");
            }
            else
            {
                // Password is incorrect
                PasswordErrorMessage = "Invalid username or password.";
                return Page();
            }

        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index"); 
        }

    }
}
