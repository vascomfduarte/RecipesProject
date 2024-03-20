﻿using Assembly.RecipeApp.Domain.Model;
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
        bool UpdateBlockStatus(User user);
        bool UpdateAdminStatus(User user);
    }
}
