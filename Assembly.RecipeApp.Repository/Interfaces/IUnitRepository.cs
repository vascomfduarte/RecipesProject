using Assembly.RecipeApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.RecipeApp.Repository.Interfaces
{
    public interface IUnitRepository
    {
        List<Unit> GetAll();
        Unit GetById(int id);
        bool Add(Unit entity, User adminUser);
        bool Update(Unit entity, User adminUser);
        bool Delete(int id, User adminUser);
    }
}
