using Assembly.RecipeApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.RecipeApp.Application.Interfaces
{
    public interface IRatingService
    {
        List<Rating> GetAll();
        Rating GetById(int id);
        bool Add(Rating entity);
        bool Update(Rating entity, User user);
        bool Delete(int id, User user);
    }
}
