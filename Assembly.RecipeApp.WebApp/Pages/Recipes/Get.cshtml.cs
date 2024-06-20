using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Application.Services;
using Assembly.RecipeApp.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Assembly.RecipeApp.WebApp.Pages.Recipes
{
    public class GetModel : PageModel
    {
        private readonly ILogger<GetModel> _logger;
        private readonly IRecipeService _recipeService;
        private readonly IRatingService _ratingService;
        private readonly ICommentService _commentService;
        private readonly INoteService _noteService;
        private readonly IUserService _userService;
        private readonly IIngredientService _ingredientService;
        private readonly IPreparationMethodService _preparationMethodService;

        [BindProperty]
        public string UserImage { get; set; }

        [BindProperty]
        public int RecipeId { get; set; }

        public User User { get; private set; }
        public Recipe Recipe { get; private set; }
        public List<Comment> Comments { get; private set; }
        public Comment Comment { get; private set; }
        public List<Note> Notes { get; private set; }
        public Note Note { get; private set; }



        public PreparationMethod PreparationMethod { get; set; }
        public List<Ingredient> RecipeIngredients { get; set; }
        public List<PreparationStep> PreparationSteps { get; set; }


        public int RatingsCounter { get; private set; }
        public int Rating { get; private set; }
        public int RatingCount { get; private set; }


        public GetModel(ILogger<GetModel> logger, 
                        IRecipeService recipeServices, 
                        IRatingService ratingService, 
                        ICommentService commentService,
                        INoteService noteService,
                        IUserService userService, 
                        IIngredientService ingredientService,
                        IPreparationMethodService preparationMethodService)
        {
            _logger = logger;
            _recipeService = recipeServices;
            _ratingService = ratingService;
            _commentService = commentService;
            _noteService = noteService;
            _userService = userService;
            _ingredientService = ingredientService;
            _preparationMethodService = preparationMethodService;
        }

        public IActionResult OnGet(int id)
        {           
            Recipe = _recipeService.GetById(id);
            Comments = _commentService.GetByRecipeId(id);
            Notes = _noteService.GetByRecipeId(id);
            RecipeIngredients = _ingredientService.GetRecipeIngredients(id);
            PreparationMethod = _preparationMethodService.GetByRecipeId(id);
            PreparationSteps = _preparationMethodService.GetStepsByRecipeId(id);

            if (Recipe.GetRecipeRating() > 0)
            {
                Rating = Recipe.GetRecipeRating();
                RatingCount = Recipe.GetRecipeRatingCount();
            }
            else
            {
                Rating = 0;
                RatingCount = 0;
            }

            if (Recipe.ImageSource == null)
            {
                Recipe.ImageSource = string.IsNullOrEmpty(User.ImageSource) ? "/images/default_recipe.png" : User.ImageSource.ToString();
            }

            return Page();
        }

        public IActionResult OnPostCreateComment(int recipeId, string commentBody)
        {
            // Handle case where user is not logged in
            var userId = HttpContext.Session.GetInt32("Id");
            if (userId is not null)
            {
                if (string.IsNullOrEmpty(commentBody))
                {
                    // Handle empty comment submission
                    return OnGet(recipeId);
                }

                // Fetch the user by id
                User = _userService.GetById(userId.Value);

                // Fetch the recipe by id using RecipeId property
                Recipe = _recipeService.GetById(recipeId);

                if (Recipe == null || User == null)
                {
                    // Handle invalid recipe or user
                    return RedirectToPage("/Error");
                }

                var comment = new Comment(commentBody, Recipe, User);

                // Add the Comment
                _commentService.Add(comment);

                // Update the Recipe
                _recipeService.Update(Recipe);
            }


            // Redirect back to the page with the updated comments
            return OnGet(recipeId);
        }

        public IActionResult OnPostEditComment(int commentId, int recipeId, string commentBody)
        {
            // Handle case where user is not logged in
            var userId = HttpContext.Session.GetInt32("Id");
            if (userId is not null)
            {
                if (string.IsNullOrEmpty(commentBody))
                {
                    // Handle empty comment submission
                    return RedirectToPage("/Error");
                }

                // Fetch the user by id
                User = _userService.GetById(userId.Value);

                // Fetch the recipe by id using RecipeId property
                Recipe = _recipeService.GetById(recipeId);

                // Fetch the comment by id
                Comment = _commentService.GetById(commentId);

                if (Recipe == null || User == null || Comment == null)
                {
                    // Handle invalid recipe or user
                    return RedirectToPage("/Error");
                }

                var comment = new Comment(commentId, commentBody, Recipe, User, Recipe.CreatedDate);

                // Update the Comment and Recipe
                _commentService.Update(comment, User);                
                _recipeService.Update(Recipe);
            }


            // Redirect back to the page with the updated comments
            return OnGet(recipeId);
        }

        public IActionResult OnPostDeleteComment(int recipeId, int commentId)
        {
            // Handle case where user is not logged in
            var userId = HttpContext.Session.GetInt32("Id");
            if (userId is not null)
            {
                // Fetch the user by id
                User = _userService.GetById(userId.Value);

                // Fetch the recipe by id using RecipeId property
                Recipe = _recipeService.GetById(recipeId);

                _commentService.Delete(commentId, User);

                // Update the comment
                _recipeService.Update(Recipe);
            }


            // Redirect back to the page with the updated comments
            return OnGet(recipeId);
        }

        public IActionResult OnPostRate(int recipeId, int rate)
        {
            // Handle case where user is not logged in
            var userId = HttpContext.Session.GetInt32("Id");
            if (userId is not null)
            {
                // Fetch the user by id
                User = _userService.GetById(userId.Value);

                // Fetch the recipe by id
                Recipe = _recipeService.GetById(recipeId);

                if (Recipe == null || User == null)
                {
                    // Handle invalid recipe or user
                    return RedirectToPage("/Error");
                }

                Rating userRating = _ratingService.GetByUserRecipeId(recipeId, User.Id);

                if (userRating != null)
                {
                    // If the user already rated the recipe, update their rating
                    userRating.Value = rate;
                    _ratingService.Update(userRating, recipeId, User.Id);
                }
                else
                {
                    // If the user hasn't rated the recipe yet, add a new rating
                    Rating newRating = new Rating(rate);
                    _ratingService.Add(newRating, recipeId, User.Id);
                }

                // Update the Recipe after rating change
                _recipeService.Update(Recipe);

            }

            // Redirect back to the page with the updated comments
            return OnGet(recipeId);
        }

        public IActionResult OnPostCreateNote(int recipeId, string noteBody)
        {
            // Handle case where user is not logged in
            var userId = HttpContext.Session.GetInt32("Id");
            if (userId is not null)
            {
                if (string.IsNullOrEmpty(noteBody))
                {
                    // Handle empty note submission
                    return OnGet(recipeId);
                }

                // Fetch the user by id
                User = _userService.GetById(userId.Value);

                // Fetch the recipe by id using RecipeId property
                Recipe = _recipeService.GetById(recipeId);

                if (Recipe == null || User == null)
                {
                    // Handle invalid recipe or user
                    return RedirectToPage("/Error");
                }

                var note = new Note(noteBody, Recipe, User);

                // Add the Note
                _noteService.Add(note);

                // Update the Recipe
                _recipeService.Update(Recipe);
            }

            // Redirect back to the page with the updated notes
            return OnGet(recipeId);
        }

        public IActionResult OnPostEditNote(int noteId, int recipeId, string noteBody)
        {
            // Handle case where user is not logged in
            var userId = HttpContext.Session.GetInt32("Id");
            if (userId is not null)
            {
                if (string.IsNullOrEmpty(noteBody))
                {
                    // Handle empty note submission
                    return RedirectToPage("/Error");
                }

                // Fetch the user by id
                User = _userService.GetById(userId.Value);

                // Fetch the recipe by id using RecipeId property
                Recipe = _recipeService.GetById(recipeId);

                // Fetch the note by id
                Note = _noteService.GetById(noteId);

                if (Recipe == null || User == null || Note == null)
                {
                    // Handle invalid recipe, user, or note
                    return RedirectToPage("/Error");
                }

                var note = new Note(noteId, noteBody, Recipe, User, Recipe.CreatedDate);

                // Update the Note and Recipe
                _noteService.Update(note, User);
                _recipeService.Update(Recipe);
            }

            // Redirect back to the page with the updated notes
            return OnGet(recipeId);
        }

        public IActionResult OnPostDeleteNote(int recipeId, int noteId)
        {
            // Handle case where user is not logged in
            var userId = HttpContext.Session.GetInt32("Id");
            if (userId is not null)
            {
                // Fetch the user by id
                User = _userService.GetById(userId.Value);

                // Fetch the recipe by id using RecipeId property
                Recipe = _recipeService.GetById(recipeId);

                _noteService.Delete(noteId, User);

                // Update the recipe
                _recipeService.Update(Recipe);
            }

            // Redirect back to the page with the updated notes
            return OnGet(recipeId);
        }

        public IActionResult OnPostToggleApprove(int recipeId)
        {
            OnGet(recipeId);

            // Retrieve UserId from session
            var userId = HttpContext.Session.GetInt32("Id");

            // Handle case where user is not logged in
            if (userId is null)
            {
                return RedirectToPage("/Users/Login");
            }

            // Fetch the user by id
            User = _userService.GetById(userId.Value);

            Recipe recipe = _recipeService.GetById(recipeId);
            if (recipe != null)
            {
                recipe.ChangeIsApproved(User, recipe);
                _recipeService.Update(recipe);
            }

            _noteService.DeleteByRecipeId(recipeId);

            OnGet(recipeId);

            // Redirect to the same page after deletion
            return Page();
        }

    }
}
