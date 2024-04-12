using Assembly.RecipeApp.Domain.Exceptions;
using Assembly.RecipeApp.Domain.Interfaces;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Assembly.RecipeApp.Domain.Model
{
    public class Product : AuditableEntity, IEntity
    {
        public int Id { get; set; }
        private string _name { get; set; }
        public string Name
        {
            get { return _name; }
            set
            {
                ValidateName(value);
                _name = value;
            }
        }

        public Product(string name)
        {
            Name = name;
            CreatedDate = DateTime.Now;            
        }

        public Product(int id, string name, DateTime createdDate)
        {
            Id = id;
            Name = name;
            CreatedDate = createdDate;
        }

        private void ValidateName(string name)
        {
            // Check if Ingredient Name is null or empty
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException("Product name cannot be null or empty.");
            }

            if (name.Length > 50)
            {
                throw new DomainException("Product name cannot exceed 50 characters.");
            }

            // Check if Ingredient name format is valid
            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
            {
                throw new DomainException("Product name can only contain letters.");
            }
        }

    }
}