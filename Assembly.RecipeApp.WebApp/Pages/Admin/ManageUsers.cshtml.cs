using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Dynamic;

namespace Assembly.RecipeApp.WebApp.Pages.Admin
{
    public class ManageUsersModel : PageModel
    {
        private readonly IUserService _userService;

        public User User { get; private set; }

        [BindProperty]
        public string UserImage { get; set; }

        public ManageUsersModel(IUserService userService)
        {
            _userService = userService;
        }

        private const int PageSize = 8; // Number of users per page
        public List<User> Users { get; set; }


        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
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

            if (User.IsAdmin is false)
            {
                return RedirectToPage("/Index");
            }

            // Set userImage property
            UserImage = string.IsNullOrEmpty(User.ImageSource) ? "/images/b750f1dc-0625-4022-9daa-7c9b1f377fdc_default-image.jpg.png" : User.ImageSource.ToString();


            // Get the total number of users
            List<User> users = _userService.GetAll();
            int totalUsers = users.Count;

            // Calculate the total number of pages
            TotalPages = (int)Math.Ceiling((double)totalUsers / PageSize);

            // Ensure page is within valid range and set CurrentPage
            CurrentPage = Math.Max(1, Math.Min(selectedPage ?? 1, TotalPages));

            // Fetch users for the current page
            Users = _userService.GetUsers(CurrentPage, PageSize);

            return Page();
        }

        public IActionResult OnGetSearch(int? selectedPage, string searchInput)
        {
            return Page();
        }

        public IActionResult OnPostToggleBlock(int userId)
        {
            OnGet(CurrentPage);

            var user = _userService.GetById(userId);
            if (user != null)
            {
                user.SwitchBlockStatus(User, user);
                _userService.Update(user);
            }

            return RedirectToPage();
        }

        public IActionResult OnPostDeleteUser(int userId)
        {
            OnGet(CurrentPage);

            var user = _userService.GetById(userId);
            if (user != null)
            {
                _userService.Delete(user);
            }

            return RedirectToPage();
        }

        public IActionResult OnPostViewDetails(int userId)
        {
            return RedirectToPage("/UserDetails", new { id = userId });
        }

    }
}
