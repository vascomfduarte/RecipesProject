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
        Difficulty GetByName(string name);
        bool Add(Difficulty entity);
        bool Update(Difficulty entity);
        bool Delete(Difficulty entity);
    }
}
