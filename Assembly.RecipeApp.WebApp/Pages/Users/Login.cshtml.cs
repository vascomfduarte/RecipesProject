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
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        public LoginModel(ILogger<LoginModel> logger, IUserService userServices)
        {
            _logger = logger;
            _userService = userServices;
        }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost() 
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            User user = _userService.Login(Username, Password);

            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetInt32("Id", user.Id);
                return RedirectToPage("/Index");
            }

            return Page(); 
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index"); 
        }

    }
}
