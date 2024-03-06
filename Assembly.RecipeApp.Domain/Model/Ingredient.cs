using Assembly.RecipeApp.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Assembly.RecipeApp.Domain.Model
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string _name { get; private set; }
        public string Name
        {
            get { return _name; }
            set
            {
                ValidateName(value);
                _name = value;
            }
        }
        public int _amount { get; private set; }
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
        //public List<Recipe> Recipes { get; set; } 

        public Ingredient(int id, string name, int amount) 
        {
            Id = id;
            Name = name;
            Amount = amount;
        }

        //public Ingredient(int id, string name, int amount, int unidId, int recipeId)
        //    :this(id, name, amount)
        //{
        //    UnitId = unidId;
        //    RecipeId = recipeId;
        //}

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
            // Check if First Name is null or empty
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException("First name cannot be null or empty.");
            }

            if (name.Length > 50)
            {
                throw new DomainException("First name cannot exceed 50 characters.");
            }

            // Check if name format is valid
            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
            {
                throw new DomainException("First name can only contain letters.");
            }
        }

    }
}
