using System.Globalization;

namespace Assembly.RecipeApp.Domain.Model
{
    public class Unit
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        //public List<Ingredient> Ingredients { get; set; }
        
        public Unit(int id, string name) 
        { 
            Id = id;
            Name = name;
        }

        //public Unit(int id, string name, List<Ingredient> ingredients) :this(id, name)
        //{ 
        //    Ingredients = ingredients;
        //}

    }
}
