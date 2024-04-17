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
        private readonly ILogger<AccountSettingsModel> _logger;
        private readonly IUserService _userService;

        [BindProperty]
        public User User { get; private set; }
        [BindProperty]
        public string UserImage { get; set; }

        public AccountSettingsModel(ILogger<AccountSettingsModel> logger, IUserService userService)
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

        public IActionResult OnPostUpdate()
        {
            // Retrieve UserId from session
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                // Handle case where user is not logged in
                return RedirectToPage("/Users/Login");
            }

            // Fetch the user by id
            User = _userService.GetById(userId.Value);

            if (User == null)
            {
                return RedirectToPage("/Index");
            }

            try
            {
                // Update user properties
                // For brevity, you may consider using a ViewModel instead of directly accessing Request.Form
                User.FirstName = Request.Form["User.FirstName"];
                User.LastName = Request.Form["User.LastName"];
                User.Email = Request.Form["User.Email"];
                User.Password = Request.Form["User.Password"];
                User.ContentBio = Request.Form["User.ContentBio"];
                User.ImageSource = Request.Form["User.ImageSource"];

                // Call the UserService method to update the user
                _userService.Update(User);

                // Redirect to the desired page upon successful update
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                // Handle exceptions
                _logger.LogError(ex, "Error updating user profile");
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return Page();
            }
        }
    }
}
