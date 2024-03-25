using Assembly.RecipeApp.Domain.Interfaces;

namespace Assembly.RecipeApp.Domain.Model
{
    public class Category : AuditableEntity, IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Recipe> Recipes { get; set; }

        public Category(string name) 
        {
            Name = name;
            CreatedDate = DateTime.Now;
        }


        public Category(int id, string name, List<Recipe> recipes, DateTime createdDate) 
        {
            Id = id;
            Name = name;
            Recipes = recipes;
            CreatedDate = createdDate;
        }

    }
}
