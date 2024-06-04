using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Application.Services;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.WebApp.Pages.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assembly.RecipeApp.WebApp.Pages.Users
{
    public class CreateUserRecipesPage2Model : PageModel
    {
        private readonly IUserService _userService;
        private readonly IRecipeService _recipeService;
        private readonly IDifficultyService _difficultyService;
        private readonly IProductService _productService;
        private readonly IUnitService _unitService;
        private readonly IIngredientService _ingredientService;
        private readonly IWebHostEnvironment _hostingEnvironment;
                      

        public Recipe Recipe { get; set; }
        public User User { get; private set; }


        public List<Difficulty> Difficulties { get; set; }


        [BindProperty]
        public PreparationMethod PreparationMethod { get; set; }
        [BindProperty]
        public List<Ingredient> RecipeIngredients { get; set; }
        [BindProperty]
        public List<Unit> Units { get; set; }
        [BindProperty]
        public List<Product> Products { get; set; }


        [BindProperty]
        public int SelectedProduct { get; set; }

        [BindProperty]
        public int IngredientAmount { get; set; }

        [BindProperty]
        public int SelectedUnit { get; set; }



        [BindProperty]
        public string UserImage { get; set; }

        public CreateUserRecipesPage2Model(IUserService userService, 
                                           IRecipeService recipeService, 
                                           IDifficultyService difficultyService, 
                                           IProductService productService, 
                                           IUnitService unitService, 
                                           IIngredientService ingredientService, 
                                           IWebHostEnvironment hostingEnvironment)
        {
            _userService = userService;
            _recipeService = recipeService;
            _difficultyService = difficultyService;
            _productService = productService;
            _unitService = unitService;
            _ingredientService = ingredientService;            
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult OnGet(int recipeId)
        {
            Difficulties = _difficultyService.GetAll();
            Products = _productService.GetAll();
            Units = _unitService.GetAll();
            RecipeIngredients = _ingredientService.GetRecipeIngredients(recipeId);
            Recipe = _recipeService.GetById(recipeId);

            // Initialize IngredientAmount
            //IngredientAmount = ingredientAmount ?? 1; // Default to 1 if ingredientAmount is null

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

            return Page();
        }

        public IActionResult OnPostAddIngredient(int recipeId)
        {
            // Fetch the product and unit objects using the IDs
            Product product = _productService.GetById(SelectedProduct);
            Unit unit = _unitService.GetById(SelectedUnit);

            Ingredient ingredient = new Ingredient(
                product: product,
                amount: IngredientAmount,
                unit: unit
            );

            _ingredientService.Add(ingredient, recipeId);

            OnGet(recipeId);

            return Page();
        }

        public IActionResult OnPostDeleteIngredient(int ingredientId, int recipeId)
        {
            // Fetch the product and unit objects using the IDs
            Ingredient ingredient = _ingredientService.GetById(ingredientId);

            _ingredientService.Delete(ingredient);

            OnGet(recipeId);

            return Page();
        }

        public IActionResult OnPostSetPreparationMethod()
        {
            // Save preparation method logic
            return Page();
        }

        public IActionResult OnPostSubmitRecipe()
        {
            return Page();
        }

    }
}

