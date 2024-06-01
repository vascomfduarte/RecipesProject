using Assembly.RecipeApp.Domain.Model;

namespace Assembly.RecipeApp.Application.Interfaces
{
    public interface IUserService : IService<User>
    {
        List<User> GetFilteredUsers(string name);
        User Login(string username, string password);
        bool UpdateAdminStatus(User user, User adminUser);
        bool UpdateFromString(string userDataString, User previousUser);
        User GetByUsername(string username);
        List<User> GetBlockedUsers(int currentPage, int pageSize);
        List<User> GetAllBlocked();
        List<User> GetUsers(int currentPage, int pageSize);
    }
}