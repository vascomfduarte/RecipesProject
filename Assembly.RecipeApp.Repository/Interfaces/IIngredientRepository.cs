using Assembly.RecipeApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.RecipeApp.Repository.Interfaces
{
    public interface IIngredientRepository : IRepository<Ingredient>
    {
        List<Ingredient> GetRecipeIngredients(int recipeId);
    }
}
