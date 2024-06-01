using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Application.Services;
using Assembly.RecipeApp.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assembly.RecipeApp.WebApp.Pages.Users
{
    public class UserCommentsModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IRecipeService _recipeService;
        private readonly ICommentService _commentService;

        public User User { get; private set; }

        [BindProperty]
        public string UserImage { get; set; }

        public UserCommentsModel(IUserService userService, IRecipeService recipeService, ICommentService commentService)
        {
            _userService = userService;
            _recipeService = recipeService;
            _commentService = commentService;
        }

        public List<Comment> Comments { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; } = 8; // Adjust as needed
        public int PreviousPage => CurrentPage > 1 ? CurrentPage - 1 : 1;
        public int NextPage => CurrentPage < TotalPages ? CurrentPage + 1 : TotalPages;

        public IActionResult OnGet(int? selectedPage)
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

            // Get all comments from the service
            var userComments = _commentService.GetByUserId(User.Id);

            // Pagination
            TotalPages = (int)Math.Ceiling((double)userComments.Count / PageSize);
            CurrentPage = selectedPage ?? 1;

            // Get comments for the current page
            Comments = userComments.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            return Page();
        }

        public IActionResult OnPostDelete(int commentId, int currentPage)
        {
            OnGet(currentPage);

            // Fetch the comment by id
            var comment = _commentService.GetById(commentId);

            // Delete the comment if it exists
            if (comment != null)
            {
                _commentService.Delete(commentId, User);
            }

            // Redirect to the same page with the current page number after deletion
            return RedirectToPage(new { selectedPage = currentPage });
        }

    }
}
