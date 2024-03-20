using Assembly.RecipeApp.Domain.Exceptions;
using Assembly.RecipeApp.Domain.Interfaces;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Assembly.RecipeApp.Domain.Model
{
    public class Unit : IEntity
    {
        public int Id { get; private set; }

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

        public Unit(int id, string name) 
        { 
            Id = id;
            Name = name;
        }

        private void ValidateName(string name)
        {
            // Check if Unit name is null or empty
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException("Unit name cannot be null or empty.");
            }

            if (name.Length > 50)
            {
                throw new DomainException("Unit name cannot exceed 50 characters.");
            }

            // Check if Unit name format is valid
            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
            {
                throw new DomainException("Unit name can only contain letters.");
            }
        }

    }
}
