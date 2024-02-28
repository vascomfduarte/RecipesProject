using Assembly.Recipe.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;

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
