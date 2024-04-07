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

        public async Task<IActionResult> OnPostSubmitAsync()
        {
            User user = new User(Request.Form["UserData.Username"],
                                Request.Form["UserData.Password"],
                                Request.Form["UserData.Email"],
                                Request.Form["UserData.FirstName"],
                                Request.Form["UserData.LastName"]);


            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}carolina98

            //// Call the UserService method to add the user
            //await _userService.AddAsync(user);

            //return RedirectToPage("/Index");

            try
            {
                // Call the UserService method to add the user
                await _userService.AddAsync(user);

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
