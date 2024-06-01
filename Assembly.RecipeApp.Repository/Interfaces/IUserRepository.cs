using Assembly.RecipeApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.RecipeApp.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        List<User> GetFilteredUsers(string name);
        User Login(string username, string password);
        User GetByUsername(string username);

        List<User> GetUsers(int currentPage, int pageSize);
        List<User> GetBlockedUsers(int currentPage, int pageSize);
    }
}
