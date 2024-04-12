using Assembly.RecipeApp.Domain.Interfaces;

namespace Assembly.RecipeApp.Domain.Model
{
    public class Difficulty : AuditableEntity, IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Recipe> Recipes { get; set; }

        public Difficulty(string name) 
        { 
            Name = name;
            CreatedDate = DateTime.Now;
        }

        public Difficulty(int id, string name, DateTime createdDate)
        {
            Id = id;
            Name = name;
            CreatedDate = createdDate;
        }

    }
}
