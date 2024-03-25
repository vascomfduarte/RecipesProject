using Assembly.RecipeApp.Domain.Exceptions;
using Assembly.RecipeApp.Domain.Interfaces;
using System.Xml.Linq;

namespace Assembly.RecipeApp.Domain.Model
{
    public class Rating : AuditableEntity, IEntity
    {
        public int Id { get; private set; }

        public double _value { get;  set; }
        public double Value
        {
            get { return _value; }
            set
            {
                ValidateValue(value);
                _value = value;
            }
        }

        public Recipe Recipe { get; set; }

        public Rating(int value)
        {
            Value = value;
            CreatedDate = DateTime.Now;
        }

        public Rating(int id, int value, DateTime createdDate)
        {
            Id = id;
            Value = value;
            CreatedDate = createdDate;
        }

        public void ValidateValue(double value)
        {

        }

    }
}