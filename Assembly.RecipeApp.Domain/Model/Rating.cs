using Assembly.RecipeApp.Domain.Exceptions;
using System.Xml.Linq;

namespace Assembly.RecipeApp.Domain.Model
{
    public class Rating
    {
        public int Id { get; private set; }
        public int Value { get; set; }

        public Recipe Recipe { get; set; }

        public Rating(int id, int value) 
        {
            Id = id;
            Value = value;
        }

    }
}