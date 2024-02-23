namespace Model
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public enum Unit 
        { 
            grams,
            kilograms,
            deciliters,
            liters,
            units,
            teaspoon, 
            tablespoon,
            cup
        }

    }
}