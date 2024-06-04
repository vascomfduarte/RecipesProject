using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Application.Services;
using Assembly.RecipeApp.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting.Internal;
using System.Text.Json;

namespace Assembly.RecipeApp.WebApp.Pages.Users
{
    public class CreateUserRecipesModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IRecipeService _recipeService;
        private readonly IDifficultyService _difficultyService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        [BindProperty]
        public IFormFile Photo { get; set; }

        [BindProperty]
        public string RecipeImage { get; set; }

        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public string Description { get; set; }

        [BindProperty]
        public int MinutesToCook { get; set; }

        [BindProperty]
        public string DifficultyChoice { get; set; }


        public Recipe Recipe { get; set; }
        public User User { get; private set; }


        public List<Difficulty> Difficulties { get; set; }


        [BindProperty]
        public string UserImage { get; set; }

        public CreateUserRecipesModel(IUserService userService, IRecipeService recipeService, IDifficultyService difficultyService, IWebHostEnvironment hostingEnvironment)
        {
            _userService = userService;
            _recipeService = recipeService;
            _difficultyService = difficultyService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult OnGet()
        {
            Difficulties = _difficultyService.GetAll();

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

            // Set Image properties
            UserImage = string.IsNullOrEmpty(User.ImageSource) ? "/images/b750f1dc-0625-4022-9daa-7c9b1f377fdc_default-image.jpg.png" : User.ImageSource.ToString();
            RecipeImage = string.IsNullOrEmpty(User.ImageSource) ? "/images/default_recipe.png" : User.ImageSource.ToString();

            return Page();
        }

        public IActionResult OnPost()
        {
            OnGet();

            // Photo
            string recipeImagePath = RecipeImage;
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

                recipeImagePath = "/images/" + uniqueFileName;
            }
            else
            {

            }

            // Difficulty
            Difficulty dif = _difficultyService.GetByName(DifficultyChoice);
            if (dif == null)
            {
                ModelState.AddModelError("DifficultyChoice", "Selected difficulty is invalid.");
                return Page();
            }

            // Create the recipe
            Recipe recipe = new Recipe(
                title: Title,
                description: Description,
                imageSource: recipeImagePath,
                minutesToCook: MinutesToCook,
                user: User,
                difficulty: dif
            );

            try
            {
                // Save the recipe to the database using service layer
                _recipeService.Add(recipe);

                Recipe = _recipeService.GetByTitle(Title);

                // Redirect to the user's account page after successfully creating the recipe
                return RedirectToPage("/Users/CreateUserRecipesPage2", new { recipeId = Recipe.Id });
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }

        }

    }
}
