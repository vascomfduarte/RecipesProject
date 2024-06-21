using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Assembly.RecipeApp.WebApp.Pages.Users
{
    public class EditUserRecipesModel : PageModel
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
        [Required(ErrorMessage = "Please enter the description.")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string RecipeDescription { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please enter the title.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }


        public string Description { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please enter the minutes to cook.")]
        [Range(1, 1440, ErrorMessage = "Minutes to cook must be between 1 and 1440.")]
        public int MinutesToCook { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please select a difficulty level.")]
        public string DifficultyChoice { get; set; }


        public Recipe Recipe { get; set; }
        public User User { get; private set; }


        public List<Difficulty> Difficulties { get; set; }


        [BindProperty]
        public string UserImage { get; set; }

        public EditUserRecipesModel(IUserService userService, IRecipeService recipeService, IDifficultyService difficultyService, IWebHostEnvironment hostingEnvironment)
        {
            _userService = userService;
            _recipeService = recipeService;
            _difficultyService = difficultyService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult OnGet(int? recipeId)
        {
            Difficulties = _difficultyService.GetAll();

            // Handle case where recipeId is null
            if (recipeId.HasValue)
            {
                Recipe = _recipeService.GetById(recipeId.Value);
                Description = RecipeDescription;
                RecipeDescription = Recipe.Description;
            }
            else
            {
                Recipe = null;
            }

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

        public IActionResult OnPostRecipe(int? recipeId)
        {
            OnGet(recipeId);

            Recipe = _recipeService.GetById(recipeId.Value);

            // Photo
            string recipeImagePath = null;
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
            else if (Photo == null)
            {
                Recipe = _recipeService.GetById(recipeId.Value);
                recipeImagePath = Recipe.ImageSource;
            }

            Difficulty dif = null;

            // Difficulty            
            if (DifficultyChoice == null)
            {
               dif = Recipe.Difficulty;
            }
            else
            {
               dif = _difficultyService.GetByName(DifficultyChoice);
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
                if (recipeId == null || recipeId == 0)
                {
                    // Save the recipe to the database using service layer
                    _recipeService.Add(recipe);
                }
                else
                {
                    Recipe updateRecipe = _recipeService.GetById(recipeId.Value);

                    if (updateRecipe == null)
                    {
                        ModelState.AddModelError(string.Empty, "Recipe not found.");
                        return Page();
                    }

                    Recipe recipe2 = new Recipe(
                        id: updateRecipe.Id,
                        title: Title,
                        description: Description,
                        imageSource: recipeImagePath,
                        minutesToCook: MinutesToCook,
                        isApproved: false,
                        createdBy: updateRecipe.CreatedBy,
                        createdDate: updateRecipe.CreatedDate,
                        difficulty: dif,
                        user: User,
                        ratings: updateRecipe.Ratings,
                        categories: updateRecipe.Categories
                     );

                    _recipeService.Update(recipe2);
                }

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

        public IActionResult OnPostDeletePhoto(int? recipeId)
        {
            OnGet(recipeId);

            Recipe = _recipeService.GetById(recipeId.Value);

            Recipe.ImageSource = null;

            _recipeService.Update(Recipe);

            
            OnGet(recipeId);

            return Page();
        }

    }
}
