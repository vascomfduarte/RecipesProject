namespace Assembly.RecipeApp.Domain.Model
{
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Ingredient> Ingredients { get; set; }
    }
}
