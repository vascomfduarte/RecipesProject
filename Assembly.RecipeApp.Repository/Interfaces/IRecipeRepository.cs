using Assembly.RecipeApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.RecipeApp.Repository.Interfaces
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        List<Recipe> GetFilteredRecipes(string name);
    }
}
