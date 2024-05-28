using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Application.Services;
using Assembly.RecipeApp.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting.Internal;

namespace Assembly.RecipeApp.WebApp.Pages.Users
{
    public class CreateUserRecipesModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IRecipeService _recipeService;
        private readonly IDifficultyService _difficultyService;
        private readonly IUnitService _unitService;
        private readonly IProductService _productService;
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


        [BindProperty]
        public List<string> ProductNames { get; set; } = new List<string>();

        [BindProperty]
        public List<float> Amounts { get; set; } = new List<float>();

        [BindProperty]
        public List<string> UnitNames { get; set; } = new List<string>();


        [BindProperty]
        public PreparationMethod PreparationMethod { get; set; }

        [BindProperty]
        public List<PreparationStep> PreparationSteps { get; set; }

        [BindProperty]
        public List<Ingredient> Ingredients { get; set; }        


        public Recipe Recipe { get; set; }
        public User User { get; private set; }


        public List<Difficulty> Difficulties { get; set; }
        public List<Unit> Units { get; set; }
        public List<Product> Products { get; set; }
        

        [BindProperty]
        public string UserImage { get; set; }

        public CreateUserRecipesModel(IUserService userService, IRecipeService recipeService, IDifficultyService difficultyService, IUnitService unitService, IProductService productService, IWebHostEnvironment hostingEnvironment)
        {
            _userService = userService;
            _recipeService = recipeService;
            _difficultyService = difficultyService;
            _unitService = unitService;
            _productService = productService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult OnGet()
        {
            Difficulties = _difficultyService.GetAll();
            Units = _unitService.GetAll();
            Products = _productService.GetAll();

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
            RecipeImage = string.IsNullOrEmpty(User.ImageSource) ? "/images/d2104ee3-a95e-4a0d-a582-b7a6952c7461_recipe-book_5228355.png" : User.ImageSource.ToString();

            return Page();
        }

        public IActionResult OnPost()
        {
            OnGet();

            string recipeImagePath = User.ImageSource;
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

            // Example: Retrieving form data from the frontend
            var title = Request.Form["Title"];
            var description = Request.Form["Description"];
            var preparationMethod = Request.Form["PreparationMethod"];
            var minutesToCook = int.Parse(Request.Form["MinutesToCook"]);

            // Example: Creating a new recipe object
            //Recipe recipe = new Recipe(title, description, preparationMethod, minutesToCook, User, ...);

            // Example: Save the recipe to the database using your service layer
            //_recipeService.Create(recipe);

            // Redirect to the user's account page after successfully creating the recipe
            return RedirectToPage("/Users/Account");
        }

    }
}
