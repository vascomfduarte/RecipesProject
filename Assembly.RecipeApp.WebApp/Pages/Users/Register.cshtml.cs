using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Assembly.RecipeApp.WebApp.Pages.Users
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;

        [BindProperty]
        [Required(ErrorMessage = "Please enter your first name.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name can only contain letters.")]
        public string FirstName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please enter your last name.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last name can only contain letters.")]
        public string LastName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please choose a username.")]
        public string Username { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please enter your password.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password must contain at least one letter and one digit.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please repeat your password.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password must contain at least one letter and one digit.")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        public string RepeatPassword { get; set; }


        public RegisterModel(IUserService userService)
        {
            _userService = userService;
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

            User user = new User(username: Username,
                             password: Password,
                             email: Email,
                             firstName: FirstName,
                             lastName: LastName);

            try
            {
                // Call the UserService method to add the user
                _userService.Add(user);

                // Fetch the user ID of the newly registered user
                user = _userService.GetByUsername(Username);

                // Set session variables for the newly registered user
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetInt32("Id", user.Id);
                HttpContext.Session.SetString("IsAdmin", user.IsAdmin ? "true" : "false");

                // Redirect to the desired page upon successful registration
                return RedirectToPage("/Users/Account");
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
