using Assembly.RecipeApp.Domain.Exceptions;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Assembly.RecipeApp.Domain.Model
{
    public class Recipe
    {
        public int Id { get; private set; }
        public string _title { get; private set; }
        public string Title
        {
            get { return _title; }
            set
            {
                ValidateTitle(value);
                _title = value;
            }
        }
        public string _instructions { get; private set; }
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
        public int _minutesToCook { get; set; }
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
        public int UserId { get; private set; }
        public int DifficultyId { get; set; }
        public DateTime CreatedAt { get; private set; }

        public User User { get; set; }
        public Difficulty Difficulty { get; set; }
        public List<Category> Categories { get; set; }
        public List<Rating> Ratings { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        //public List<RecipeIngredients> RecipeIngredients { get; set; }
        //public List<RecipeCategory> RecipeCategories { get; set; }

        public Recipe(string title, string instructions, int minutesToCook, int userId, int difficultyId)
        {
            Title = title;
            Instructions = instructions;
            MinutesToCook = minutesToCook;
            UserId = userId;
            DifficultyId = difficultyId;
            CreatedAt = DateTime.Now.Date;
        }

        public Recipe(string title, string instructions, string imageSource, int minutesToCook, int userId, int difficultyId)
            : this(title, instructions, minutesToCook, userId, difficultyId)
        {
            ImageSource = imageSource;
            IsApproved = false;
        }

        // Used when retriving data from the database
        public Recipe(int id, string title, string instructions, string imageSource, int minutesToCook, bool isApproved, int userId, int difficultyId, DateTime createdAt)
        {
            Id = id;
            Title = title;
            Instructions = instructions;
            MinutesToCook = minutesToCook;
            UserId = userId;
            DifficultyId = difficultyId;
            ImageSource = imageSource;
            IsApproved = isApproved;
            CreatedAt = createdAt;
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
