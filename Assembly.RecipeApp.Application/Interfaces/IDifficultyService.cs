using Assembly.RecipeApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.RecipeApp.Application.Interfaces
{
    public interface IDifficultyService
    {
        List<Ingredient> GetAll();
        Ingredient GetById(int id);
        bool Add(Difficulty entity, User adminUser);
        bool Update(Difficulty entity, User adminUser);
        bool Delete(int id, User adminUser);
    }
}
