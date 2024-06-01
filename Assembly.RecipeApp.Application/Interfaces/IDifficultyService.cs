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
        List<Difficulty> GetAll();
        Difficulty GetById(int id);
        Difficulty GetByName(string name);
        bool Add(Difficulty entity, User adminUser);
        bool Update(Difficulty entity, User adminUser);
        bool Delete(Difficulty entity, User adminUser);
    }
}
