using Assembly.RecipeApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.RecipeApp.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category GetById(int id);
        bool Add(Category entity, User adminUser);
        bool Update(Category entity, User adminUser);
        bool Delete(int id, User adminUser);
    }
}
