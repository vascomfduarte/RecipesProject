using Assembly.RecipeApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.RecipeApp.Repository.Interfaces
{
    public interface IRatingRepository
    {
        List<Rating> GetAll();
        Rating GetById(int id);
        bool Add(Rating entity, User adminUser);
        bool Update(Rating entity, User adminUser);
        bool Delete(int id, User adminUser);
    }
}
