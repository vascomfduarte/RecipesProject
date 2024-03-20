using Assembly.RecipeApp.Domain.Exceptions;
using Assembly.RecipeApp.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace Assembly.RecipeApp.Domain.Model
{
    public class Ingredient : IEntity
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

        private int _amount { get; set; }
        public int Amount
        {
            get { return _amount; }
            set
            {
                ValidateAmount(value);
                _amount = value;
            }
        }

        public Unit Unit { get; set; }

        public Ingredient(int id, string name, int amount) 
        {
            Id = id;
            Name = name;
            Amount = amount;
        }

        public Ingredient(int id, string name, int amount, Unit unit)
            : this(id, name, amount)
        {
            Unit = unit;
        }

        private void ValidateAmount(int value)
        {
            if (value < 0 || value > 1000)
            {
                throw new ArgumentOutOfRangeException(nameof(Amount), "Amount must be a number between 0 and 1000.");
            }
        }

        private void ValidateName(string name)
        {
            // Check if Ingredient Name is null or empty
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException("Ingredient name cannot be null or empty.");
            }

            if (name.Length > 50)
            {
                throw new DomainException("Ingredient name cannot exceed 50 characters.");
            }

            // Check if Ingredient name format is valid
            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
            {
                throw new DomainException("Ingredient name can only contain letters.");
            }
        }

    }
}
