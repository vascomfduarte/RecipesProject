using Assembly.RecipeApp.Domain.Interfaces;

namespace Assembly.RecipeApp.Domain.Model
{
    public class PreparationMethod : AuditableEntity, IEntity
    {
        public int Id { get; set; }
        public List<PreparationStep> Steps { get; set; }

        public PreparationMethod(List<PreparationStep> steps) 
        {
            Steps = steps;
        }

        public PreparationMethod(int id, List<PreparationStep> steps) : this (steps)
        {
            Id = id;
        }

    }
}