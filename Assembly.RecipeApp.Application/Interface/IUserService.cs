using Assembly.RecipeApp.Domain.Model;

namespace Assembly.RecipeApp.Application.Interface
{
    public interface IUserService : IService<User>
    {
        List<User> GetFilteredUsers(string name);
        bool UpdateIsAdmin(User adminUser, User userToModify);
        bool UpdateIsBlocked(User adminUser, User userToModify);
    }

}