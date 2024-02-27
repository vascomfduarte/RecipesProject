
namespace Model
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public int UnitId { get; set; }

        public Unit Unit { get; set; }
        public List<Recipe> Recipes { get; set; }

    }
}