using Assembly.RecipeApp.Domain.Exceptions;
using Assembly.RecipeApp.Domain.Interfaces;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Assembly.RecipeApp.Domain.Model
{
    public class Recipe : IEntity
    {
        public int Id { get; private set; }

        private string _title { get; set; }
        public string Title
        {
            get { return _title; }
            set
            {
                ValidateTitle(value);
                _title = value;
            }
        }

        private string _instructions { get; set; }
        public string Instructions
        {
            get { return _instructions; }
            set
            {
                ValidateInstructions(value);
                _instructions = value;
            }
        }

        public string ImageSource { get; set; }

        private int _minutesToCook { get; set; }
        public int MinutesToCook
        {
            get { return _minutesToCook; }
            set
            {
                ValidateMinutesToCook(value);
                _minutesToCook = value;
            }
        }

        public bool IsApproved { get; private set; }

        public Difficulty Difficulty { get; set; } // Obrigatório

        public DateTime CreatedDate { get; private set; }

        public User User { get; set; } // Obrigatório
        public List<Category> Categories { get; set; } // Obrigatório
        public List<Rating> Ratings { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Ingredient> Ingredients { get; set; } // Obrigatório

        public Recipe(string title, string instructions, int minutesToCook, User user, Difficulty difficulty, List<Ingredient> ingredients)
        {
            Title = title;
            Instructions = instructions;
            MinutesToCook = minutesToCook;            
            Difficulty = difficulty;
            CreatedDate = DateTime.Now.Date;
            User = user;
            Ingredients = ingredients;
        }

        public Recipe(string title, string instructions, string imageSource, int minutesToCook, User user, Difficulty difficulty, List<Ingredient> ingredients)
            : this(title, instructions, minutesToCook, user, difficulty, ingredients)
        {
            ImageSource = imageSource;
            IsApproved = false;
        }

        // Used when retriving data from the database
        public Recipe(int id, string title, string instructions, string imageSource, int minutesToCook, bool isApproved, User user, Difficulty difficulty, DateTime createdDate, List<Ingredient> ingredients)
        {
            Id = id;
            Title = title;
            Instructions = instructions;
            MinutesToCook = minutesToCook;            
            Difficulty = difficulty;
            ImageSource = imageSource;
            IsApproved = isApproved;
            CreatedDate = createdDate;
            User = user;
            Ingredients = ingredients;
            // Tenho de adicionar Comments, Ratings, Categories
        }

        private void ValidateTitle(string value)
        {
            // Check if title is null or empty
            if (string.IsNullOrEmpty(value))
            {
                throw new DomainException("Recipe title cannot be null or empty.");
            }

            // Check if title length exceeds maximum allowed characters
            if (value.Length > 100)
            {
                throw new DomainException("Recipe title cannot exceed 100 characters.");
            }

            // Check if title contains any special characters
            if (!Regex.IsMatch(value, @"^[a-zA-Z0-9\s]+$"))
            {
                throw new DomainException("Recipe title can only contain letters, numbers, and spaces.");
            }
        }
        private void ValidateInstructions(string value)
        {
            // Check if instructions are null or empty
            if (string.IsNullOrEmpty(value))
            {
                throw new DomainException("Recipe instructions cannot be null or empty.");
            }

            // Check if instructions length exceeds maximum allowed characters
            if (value.Length > 1000)
            {
                throw new DomainException("Recipe instructions cannot exceed 1000 characters.");
            }
        }
        private void ValidateMinutesToCook(int value)
        {
            // Check if minutes to cook is negative
            if (value < 0)
            {
                throw new DomainException("Minutes to cook cannot be negative.");
            }

            // Check if minutes to cook is too long
            if (value > 1440)
            {
                throw new DomainException("Minutes to cook cannot exceed 1440 minutes.");
            }
        }

        public void SetIsApproved(Recipe recipe, bool isApproved)
        {
            // Check if the current user is an admin
            if (recipe != null)
            {
                IsApproved = isApproved;
            }
            else
            {
                throw new DomainException("Unable to change IsApproved status.");
            }
        }

        /// <summary>
        /// Method to allow an admin user to approve a recipe.
        /// </summary>
        /// <param name="currentUser"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void ChangeIsApproved(User currentUser, Recipe recipe)
        {
            if (currentUser == null || !currentUser.IsAdmin)
            {
                throw new DomainException("Only admin users can approve recipes.");
            }
            else if (recipe != null && currentUser.IsAdmin)
            {
                recipe.IsApproved = !recipe.IsApproved;
            }
            else
            {
                throw new ArgumentNullException(nameof(recipe), "Recipe cannot be null.");
            }
        }
    }
}
