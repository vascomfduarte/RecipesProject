using Assembly.RecipeApp.Domain.Model;
using System.Dynamic;

namespace Assembly.RecipeApp.Repository.Interfaces
{
    public interface IPreparationMethodRepository
    {
        PreparationMethod GetByRecipeId(int id);
    }
}
