using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assembly.RecipeApp.WebApp.Pages.Admin
{
    public class ManageUnitsModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IUnitService _unitService;

        public User User { get; private set; }

        [BindProperty]
        public string UserImage { get; set; }

        public ManageUnitsModel(IUserService userService, IUnitService unitService)
        {
            _userService = userService;
            _unitService = unitService;
        }

        public List<Unit> Units { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; } = 8; // Adjust as needed
        public int PreviousPage => CurrentPage > 1 ? CurrentPage - 1 : 1;
        public int NextPage => CurrentPage < TotalPages ? CurrentPage + 1 : TotalPages;

        [BindProperty]
        public int? EditingUnitId { get; set; }

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

            // Get all recipes from the service
            var allUnits = _unitService.GetAll();

            // Pagination
            TotalPages = (int)Math.Ceiling((double)allUnits.Count / PageSize);
            CurrentPage = selectedPage ?? 1;

            // Get recipes for the current page
            Units = allUnits.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            return Page();
        }

        public IActionResult OnPostDeleteUnit(int unitId, int currentPage)
        {
            OnGet(CurrentPage);

            var unit = _unitService.GetById(unitId);
            if (unit != null)
            {
                _unitService.Delete(unitId, User);
            }

            // Redirect to the same page after deletion
            return RedirectToPage(new { selectedPage = currentPage });
        }

        public IActionResult OnPostEditUnit(int unitId, int currentPage)
        {
            EditingUnitId = unitId;
            return OnGet(currentPage);
        }

        public IActionResult OnPostUpdateUnit(int unitId, string newName)
        {
            OnGet(CurrentPage);

            var unit = _unitService.GetById(unitId);
            if (unit != null)
            {
                unit.Name = newName;
                _unitService.Update(unit, User);
            }

            EditingUnitId = null;
            return RedirectToPage(new { selectedPage = CurrentPage });
        }

        public IActionResult OnPostCancelEdit(int currentPage)
        {
            EditingUnitId = null;
            OnGet(currentPage); // Reload the page to reflect the cancellation of edit mode
            return Page();
        }
    }
}
