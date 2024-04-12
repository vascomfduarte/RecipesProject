using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assembly.RecipeApp.WebApp.Pages.Users
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;

        public User UserData { get; set; }


        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostSubmit()
        {
            User user = new User(username: Request.Form["UserData.Username"],
                                 password: Request.Form["UserData.Password"],
                                 email: Request.Form["UserData.Email"],
                                 firstName: Request.Form["UserData.FirstName"],
                                 lastName: Request.Form["UserData.LastName"]);
                     
            try
            {
                // Call the UserService method to add the user
                _userService.Add(user);

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
