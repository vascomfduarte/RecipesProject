using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Assembly.RecipeApp.WebApp.Pages.Users
{
    public class SecurityModel : PageModel
    {
        private readonly ILogger<SecurityModel> _logger;
        private readonly IUserService _userService;

        public User User { get; private set; }

        [BindProperty]
        [Required(ErrorMessage = "Please enter your current password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please enter your new password.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password must contain at least one letter and one digit.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please repeat your new password.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password must contain at least one letter and one digit.")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }

        [BindProperty]
        public string ContentBio { get; set; }

        [BindProperty]
        public string UserImage { get; set; }

        public SecurityModel(ILogger<SecurityModel> logger, IUserService userService)
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

        public IActionResult OnPost()
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
                return RedirectToPage("/Users/Login");
            }

            // Check if the entered password matches the user's current password
            if (Password != User.Password)
            {
                // Passwords don't match, return an error or handle the case accordingly
                ModelState.AddModelError("Password", "Current password is incorrect.");
                OnGet();
                return Page();
            }

            // Check if the new password and its confirmation match
            if (NewPassword != ConfirmNewPassword)
            {
                // New password and confirmation don't match, return an error or handle the case accordingly
                ModelState.AddModelError("ConfirmNewPassword", "The new password and confirmation password do not match.");
                OnGet();
                return Page();
            }

            User user = new User(username: User.Username,
                                 password: NewPassword,
                                 email: User.Email,
                                 firstName: User.FirstName,
                                 lastName: User.LastName,
                                 contentBio: User.ContentBio,
                                 imageSource: User.ImageSource);

            // Call the user service to update the user
            _userService.Update(user);

            // Fetch the user ID of the newly registered user
            User = _userService.GetByUsername(user.Username);

            // Set session variables with the updated user
            HttpContext.Session.SetString("Username", User.Username);
            HttpContext.Session.SetInt32("Id", User.Id);

            return RedirectToPage("/Users/Account");
        }

    }
}
