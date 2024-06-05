using Assembly.RecipeApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.RecipeApp.Application.Interfaces
{
    public interface IPreparationMethodService : IService<PreparationMethod>
    {
        PreparationMethod GetByRecipeId(int recipeId);
        List<PreparationStep> GetStepsByRecipeId(int recipeId);
        PreparationStep GetStepById(int stepId);
        bool Add(PreparationMethod entity, int recipeId);
        bool AddStep(PreparationStep entity, int recipeId);
        bool DeleteStep(PreparationStep entity);
    }
}
