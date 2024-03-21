using Assembly.RecipeApp.Domain.Exceptions;
using Assembly.RecipeApp.Domain.Interfaces;

namespace Assembly.RecipeApp.Domain.Model
{
    public class PreparationStep : AuditableEntity, IEntity
    {
        public int Id { get; set; }

        public int Order { get; set; }

        private string _description { get; set; }
        public string Description
        {
            get { return _description; }
            set
            {
                ValidateDescription(value);
                _description = value;
            }
        }

        public string ImageSource { get; set; }

        public PreparationStep (int order, string description)
        {
            Order = order;
            Description = description;
        }

        public PreparationStep (int order, string description, string imageSource) : this (order, description) 
        { 
            ImageSource = imageSource;
        }

        public PreparationStep (int id, int order, string description, string imageSource) : this(order, description, imageSource)
        {
            Id = id;
        }

        private void ValidateDescription(string value)
        {
            // Check if instructions are null or empty
            if (string.IsNullOrEmpty(value))
            {
                throw new DomainException("Description cannot be null or empty.");
            }

            // Check if instructions length exceeds maximum allowed characters
            if (value.Length > 1000)
            {
                throw new DomainException("Description cannot exceed 1000 characters.");
            }
        }
    }
}