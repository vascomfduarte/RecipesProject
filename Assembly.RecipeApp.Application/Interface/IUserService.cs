using Assembly.RecipeApp.Domain.Model;

namespace Assembly.RecipeApp.Application.Interface
{
    public interface IUserService : IService<User>
    {
        List<User> GetFilteredUsers(string name);
    }

}
