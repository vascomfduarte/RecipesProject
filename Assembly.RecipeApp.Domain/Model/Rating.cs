namespace Assembly.RecipeApp.Domain.Model
{
    public class Rating
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }
    }
}