using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.WebApp.Pages.Recipes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting.Internal;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace Assembly.RecipeApp.WebApp.Pages.Users
{
    public class InfoModel : PageModel
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<InfoModel> _logger;
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
        public string ContentBio { get; set; }

        [BindProperty]
        public string UserImage { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        public InfoModel(ILogger<InfoModel> logger, IUserService userService, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _userService = userService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult OnGet()
        {
            // Retrieve UserId from session
            var userId = HttpContext.Session.GetInt32("Id");

            // Handle case where user is not logged in
            if (userId is null)
            {
                return RedirectToPage("/Users/Login");
            }

            // Fetch the user by id
            User = _userService.GetById(userId.Value);

            // Handle case where user is not logged in
            if (User is null)
            {
                return RedirectToPage("/Users/Login");
            }

            // Set userImage property
            UserImage = string.IsNullOrEmpty(User.ImageSource) ? "/images/b750f1dc-0625-4022-9daa-7c9b1f377fdc_default-image.jpg.png" : User.ImageSource.ToString();
     
            return Page();
        }

        public IActionResult OnPost()
        {
            OnGet();

            string userImagePath = User.ImageSource;
            if (Photo != null && Photo.Length > 0)
            {
                var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Ensure the uploads folder exists
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }

                userImagePath = "/images/" + uniqueFileName;
            }

            User user = new User(
                id: User.Id,
                username: User.Username,
                password: User.Password,
                email: string.IsNullOrEmpty(Email) ? User.Email : Email,
                firstName: string.IsNullOrEmpty(FirstName) ? User.FirstName : FirstName,
                lastName: string.IsNullOrEmpty(LastName) ? User.LastName : LastName,
                contentBio: string.IsNullOrEmpty(ContentBio) ? User.ContentBio : ContentBio,
                imageSource: userImagePath,
                isAdmin: User.IsAdmin,
                isBlocked: User.IsBlocked,
                createdDate: User.CreatedDate
            );

            // Call the user service to update the user
            _userService.Update(user);

            // Fetch the user ID of the newly registered user
            User = _userService.GetByUsername(user.Username);

            // Set session variables with the updated user
            HttpContext.Session.SetString("Username", User.Username);
            HttpContext.Session.SetInt32("Id", User.Id);

            OnGet();
            return RedirectToPage("/Users/Account");
        }

        public IActionResult OnPostDeletePhoto()
        {
            var userId = HttpContext.Session.GetInt32("Id");
            if (userId == null)
            {
                return RedirectToPage("/Users/Login");
            }

            User = _userService.GetById(userId.Value);
            if (User == null)
            {
                return RedirectToPage("/Users/Login");
            }

            if (!string.IsNullOrEmpty(User.ImageSource))
            {
                var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, User.ImageSource.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                User user = new User(
                    username: User.Username,
                    password: User.Password,
                    email: string.IsNullOrEmpty(Email) ? User.Email : Email,
                    firstName: string.IsNullOrEmpty(FirstName) ? User.FirstName : FirstName,
                    lastName: string.IsNullOrEmpty(LastName) ? User.LastName : LastName,
                    contentBio: string.IsNullOrEmpty(ContentBio) ? User.ContentBio : ContentBio,
                    imageSource: null
                );

                // Call the user service to update the user
                _userService.Update(user);
            }

            return RedirectToPage("/Users/Info");
        }

    }
}
