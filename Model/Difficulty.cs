
namespace Model
{
    public class Difficulty
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation properties
        public List<Recipe> Recipes { get; set; }
    }
}