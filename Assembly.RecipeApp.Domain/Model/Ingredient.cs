using Assembly.RecipeApp.Domain.Exceptions;
using Assembly.RecipeApp.Domain.Interfaces;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace Assembly.RecipeApp.Domain.Model
{
    public class Ingredient : AuditableEntity, IEntity
    {
        public int Id { get; set; }        

        private double _amount { get; set; }
        public double Amount
        {
            get { return _amount; }
            set
            {
                ValidateAmount(value);
                _amount = value;
            }
        }

        public Product Product { get; set; }
        public Unit Unit { get; set; }

        public Ingredient(Product product, double amount, Unit unit) 
        {
            Product = product;
            Amount = amount;
            Unit = unit;
            CreatedDate = DateTime.Now;
        }

        public Ingredient(int id, Product product, double amount, Unit unit, DateTime createdDate)
        {
            Id = id;
            Product = product;
            Amount = amount;
            Unit = unit;
            CreatedDate = createdDate;
        }

        private void ValidateAmount(double value)
        {
            if (value < 0 || value > 1000)
            {
                throw new ArgumentOutOfRangeException(nameof(Amount), "Amount must be a number between 0 and 1000.");
            }
        }



    }
}
