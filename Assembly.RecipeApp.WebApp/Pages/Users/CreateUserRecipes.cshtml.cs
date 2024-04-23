using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Application.Services;
using Assembly.RecipeApp.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assembly.RecipeApp.WebApp.Pages.Users
{
    public class CreateUserRecipesModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IRecipeService _recipeService;
        private readonly IDifficultyService _difficultyService;
        private readonly IUnitService _unitService;
        private readonly IProductService _productService;

        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public string Description { get; set; }

        [BindProperty]
        public int MinutesToCook { get; set; }

        [BindProperty]
        public string RecipeImage { get; set; }

        [BindProperty]
        public string DifficultyChoice { get; set; }  

        [BindProperty]
        public string ProductName { get; set; }

        [BindProperty]
        public string UnitName { get; set; }

        [BindProperty]
        public PreparationMethod PreparationMethod { get; set; }
        
        public List<PreparationStep> PreparationSteps { get; set; }
        public List<Ingredient> Ingredients { get; set; }        
        public Recipe Recipe { get; set; }

        public List<Difficulty> Difficulties { get; set; }
        public List<Unit> Units { get; set; }
        public List<Product> Products { get; set; }

        public User User { get; private set; }
        

        [BindProperty]
        public string UserImage { get; set; }

        public CreateUserRecipesModel(IUserService userService, IRecipeService recipeService, IDifficultyService difficultyService, IUnitService unitService, IProductService productService)
        {
            _userService = userService;
            _recipeService = recipeService;
            _difficultyService = difficultyService;
            _unitService = unitService;
            _productService = productService;
        }

        public IActionResult OnGet()
        {
            Difficulties = _difficultyService.GetAll();
            Units = _unitService.GetAll();
            Products = _productService.GetAll();

            // Retrieve UserId from session
            var userId = HttpContext.Session.GetInt32("Id");

            // Fetch the user by id
            User = _userService.GetById(userId.Value);

            // Handle case where user is not logged in
            if (User is null)
            {
                return RedirectToPage("/Users/Login");
            }

            // Set userImage property
            UserImage = string.IsNullOrEmpty(User.ImageSource) ? "https://i.imgur.com/UtPRmE0.png" : User.ImageSource.ToString();

            return Page();
        }

        public IActionResult OnPost()
        {
            OnGet();          
            
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
