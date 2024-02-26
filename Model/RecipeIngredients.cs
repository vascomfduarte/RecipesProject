namespace Model
{
    public class RecipeIngredients
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public int Amount { get; set; }
        public int UnitId { get; set; }
    }
}