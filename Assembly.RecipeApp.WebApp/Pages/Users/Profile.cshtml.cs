using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.WebApp.Pages.Recipes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Assembly.RecipeApp.WebApp.Pages.Users
{
    public class ProfileModel : PageModel
    {
        private readonly ILogger<GetAllModel> _logger;
        private readonly IUserService _userService;
                
        public User User { get; private set; }

        [BindProperty]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name can only contain letters.")]
        public string FirstName { get; set; }

        [BindProperty]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last name can only contain letters.")]
        public string LastName { get; set; }

        [BindProperty]
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

        [BindProperty]
        public string ContentBio { get; set; }

        [BindProperty]
        public string UserImage { get; set; }

        public ProfileModel(ILogger<GetAllModel> logger, IUserService userService)
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

            User user = new User(username: User.Username,
                             password: string.IsNullOrEmpty(Password) ? User.Password : Password,
                             email: string.IsNullOrEmpty(Email) ? User.Email : Email,
                             firstName: string.IsNullOrEmpty(FirstName) ? User.FirstName : FirstName,
                             lastName: string.IsNullOrEmpty(LastName) ? User.LastName : LastName,
                             contentBio: string.IsNullOrEmpty(ContentBio) ? User.ContentBio : ContentBio,
                             imageSource: string.IsNullOrEmpty(UserImage) ? User.ImageSource : UserImage);

            // Call the user service to update the user
            _userService.Update(user);

            // Fetch the user ID of the newly registered user
            User = _userService.GetByUsername(user.Username);

            // Set session variables with the updated user
            HttpContext.Session.SetString("Username", User.Username);
            HttpContext.Session.SetInt32("Id", User.Id);

            return Page();
        }
        
        public IActionResult OnPostSecurity()
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

            User user = new User(username: User.Username,
                             password: string.IsNullOrEmpty(Password) ? User.Password : Password,
                             email: string.IsNullOrEmpty(Email) ? User.Email : Email,
                             firstName: string.IsNullOrEmpty(FirstName) ? User.FirstName : FirstName,
                             lastName: string.IsNullOrEmpty(LastName) ? User.LastName : LastName,
                             contentBio: string.IsNullOrEmpty(ContentBio) ? User.ContentBio : ContentBio,
                             imageSource: string.IsNullOrEmpty(UserImage) ? User.ImageSource : UserImage);

            // Call the user service to update the user
            _userService.Update(user);

            // Fetch the user ID of the newly registered user
            User = _userService.GetByUsername(user.Username);

            // Set session variables with the updated user
            HttpContext.Session.SetString("Username", User.Username);
            HttpContext.Session.SetInt32("Id", User.Id);

            return Page();
        }


    }
}
