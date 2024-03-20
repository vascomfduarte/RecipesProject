using Assembly.RecipeApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.RecipeApp.Repository.Interfaces
{
    public interface IDifficultyRepository
    {
        List<Difficulty> GetAll();
        Difficulty GetById(int id);
        bool Add(Difficulty entity, User adminUser);
        bool Update(Difficulty entity, User adminUser);
        bool Delete(int id, User adminUser);
    }
}
