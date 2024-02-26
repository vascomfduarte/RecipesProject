
namespace Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Recipe> Recipes { get; set; }
        //public List<RecipeCategory> RecipeCategories { get; set; }

    }
}