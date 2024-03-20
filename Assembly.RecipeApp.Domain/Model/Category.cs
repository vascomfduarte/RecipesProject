using Assembly.RecipeApp.Domain.Interfaces;

namespace Assembly.RecipeApp.Domain.Model
{
    public class Category : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Recipe> Recipes { get; set; }

        public Category(int id, string name) 
        {
            Id = id;
            Name = name;
        }

    }
}
