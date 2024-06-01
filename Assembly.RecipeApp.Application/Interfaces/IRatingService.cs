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
        Rating GetByUserRecipeId(int recipeId, int userId);
        bool Add(Rating entity);
        bool Add(Rating entity, int recipeId, int userId);
        bool Update(Rating entity, User user);
        bool Update(Rating entity, int recipeId, int userId);
        bool Delete(int id, User user);
    }
}
