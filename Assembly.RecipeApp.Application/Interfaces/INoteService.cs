using Assembly.RecipeApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.RecipeApp.Application.Interfaces
{
    public interface INoteService
    {
        List<Note> GetAll();
        Note GetById(int id);
        List<Note> GetByRecipeId(int id);
        List<Note> GetByUserId(int id);
        bool Add(Note entity);
        bool Update(Note entity, User user);
        bool Delete(int id, User user);
        bool DeleteByRecipeId(int recipeId);
    }

}
