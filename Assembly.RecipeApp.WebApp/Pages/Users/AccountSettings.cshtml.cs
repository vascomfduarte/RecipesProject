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

        public string userImage;

        public AccountSettingsModel(ILogger<GetAllModel> logger, IUserService userService)
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

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            User user = new User(username: User.Username,
                                 password: Request.Form["UserData.Password"],                                 
                                 email: Request.Form["UserData.Email"],
                                 firstName: Request.Form["UserData.FirstName"],
                                 lastName: Request.Form["UserData.LastName"],
                                 contentBio: Request.Form["UserData.ContentBio"],
                                 imageSource: Request.Form["UserData.ImageSource"]);

            try
            {
                // Call the UserService method to add the user
                await _userService.UpdateAsync(user);

                // Redirect to the desired page upon successful registration
                return RedirectToPage("/Index");
            }
            catch (ArgumentException ex)
            {
                // Handle any specific exceptions thrown by UserService, if needed
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
            catch (Exception ex)
            {
                // Handle other unexpected exceptions
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return Page();
            }
        }
    }
}
