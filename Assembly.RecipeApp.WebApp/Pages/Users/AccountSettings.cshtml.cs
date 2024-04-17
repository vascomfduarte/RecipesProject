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
        private readonly ILogger<GetAllModel> _logger;
        private readonly IUserService _userService;

        public User User { get; private set; }

        public int UserId { get; private set; }

        public string userImage;

        public AccountSettingsModel(ILogger<GetAllModel> logger, IUserService userService)
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

        public IActionResult OnPostUpdate()
        {
            User = _userService.GetById(UserId);

            if (User == null)
            {
                return RedirectToPage("/Index");
            }

            try
            {
                // Construct a string representation of the user object
                string newUserDataString = $"{Request.Form["User.Username"]}|{Request.Form["User.Password"]}|{Request.Form["User.Email"]}|{Request.Form["User.FirstName"]}|{Request.Form["User.LastName"]}|{Request.Form["User.ContentBio"]}|{Request.Form["User.ImageSource"]}";

                // Call the UserService method to update the user
                _userService.UpdateFromString(newUserDataString, User);

                // Redirect to the desired page upon successful update
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                // Handle exceptions
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return Page();
            }
        }
    }
}
