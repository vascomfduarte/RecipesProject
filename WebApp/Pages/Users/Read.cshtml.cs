using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;
using Services;

namespace WebApp.Pages.Users
{
    public class ReadModel : PageModel
    {
        public List<User> User { get; set; }

        private static UserServices _userServices = new UserServices();

        public void OnGet(string parameter)
        {
            User = _userServices.GetUserByStatus(parameter);
        }
    }
}
