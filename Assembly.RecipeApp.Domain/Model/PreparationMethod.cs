using Assembly.RecipeApp.Domain.Interfaces;

namespace Assembly.RecipeApp.Domain.Model
{
    public class PreparationMethod
    {
        public List<PreparationStep> Steps { get; set; }

        public PreparationMethod() // Parameterless constructor
        {
            Steps = new List<PreparationStep>();
        }

        public PreparationMethod(List<PreparationStep> steps) 
        {
            Steps = steps;
        }
    }
}