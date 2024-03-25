namespace Assembly.RecipeApp.Domain.Model
{
    public class PreparationMethod
    {
        public int Id { get; set; }
        public List<PreparationStep> Steps { get; set; }

        public PreparationMethod(List<PreparationStep> steps) 
        {
            Steps = steps;
        }

        public PreparationMethod(int id, List<PreparationStep> steps)
        {
            Id = id;
            Steps = steps;
        }

    }
}