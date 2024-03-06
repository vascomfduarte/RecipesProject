using Assembly.RecipeApp.Domain.Model;

namespace Assembly.RecipeApp.Application.Interfaces
{
    public interface IUserService : IService<User>
    {
        List<User> GetFilteredUsers(string name);
        User Login(string username, string password);
        bool UpdateBlockStatus(User user, User adminUser);
        bool UpdateAdminStatus(User user, User adminUser);
    }
}