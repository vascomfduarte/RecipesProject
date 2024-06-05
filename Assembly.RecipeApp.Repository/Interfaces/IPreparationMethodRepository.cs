using Assembly.RecipeApp.Domain.Model;
using System.Dynamic;

namespace Assembly.RecipeApp.Repository.Interfaces
{
    public interface IPreparationMethodRepository
    {
        PreparationMethod GetByRecipeId(int id);
        List<PreparationStep> GetStepsByRecipeId(int recipeId);
        PreparationStep GetStepById(int stepId);
        bool DeleteByRecipeId(int recipeId);
        bool DeleteStep(PreparationStep entity);
        bool Add(PreparationMethod entity, int recipeId);
        bool AddStep(PreparationStep entity, int recipeId);
    }
}
