using Assembly.RecipeApp.Domain.Exceptions;
using Assembly.RecipeApp.Domain.Interfaces;
using System.Collections.Generic;
using System.Net.Cache;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Assembly.RecipeApp.Domain.Model
{
    public class Recipe : AuditableEntity, IEntity
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
        public User User { get; set; } // Obrigatório

        public PreparationMethod PreparationMethod { get; set; }
        public List<Ingredient> Ingredients { get; set; }

        public List<Category> Categories { get; set; }
        public List<Rating> Ratings { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Note> Notes { get; set; }

        public Recipe(string title, string description, string imageSource, int minutesToCook, User user, Difficulty difficulty)
        {
            Title = title;
            Description = description;
            ImageSource = imageSource;
            MinutesToCook = minutesToCook;
            Difficulty = difficulty;
            User = user;
            CreatedBy = user.Username;
            CreatedDate = DateTime.Now;
        }    

        public Recipe(string title, string description, string imageSource, PreparationMethod preparationMethod, int minutesToCook, User user, Difficulty difficulty, List<Ingredient> ingredients)
            : this(title, description, imageSource, minutesToCook, user, difficulty)
        {
            PreparationMethod = preparationMethod;
            Ingredients = ingredients;
        }

        public Recipe(string title, string description, PreparationMethod preparationMethod, string imageSource, int minutesToCook, User user, Difficulty difficulty, List<Ingredient> ingredients)
            : this(title, description, imageSource, preparationMethod, minutesToCook, user, difficulty, ingredients)
        {
            
            IsApproved = false;
        }

        // Used when retriving data from the database
        public Recipe(int id, string title, string description, string imageSource, int minutesToCook, bool isApproved, User user, Difficulty difficulty, List<Rating> ratings, List<Category> categories, string createdBy, DateTime createdDate) 
        { 
            Id = id;
            Title = title;
            Description = description;
            ImageSource = imageSource;
            MinutesToCook = minutesToCook;
            IsApproved = isApproved;
            CreatedBy = createdBy;
            CreatedDate = createdDate;
            Difficulty = difficulty;
            User = user;
            Ratings = ratings;
            Categories = categories;
        }

        public Recipe(int id, string title, string description, PreparationMethod preparationMethod, string imageSource, int minutesToCook, bool isApproved, User user, Difficulty difficulty, List<Rating> ratings, List<Category> categories, List<Ingredient> ingredients, List<Comment> comments, List<Note> notes, string createdBy, DateTime createdDate)
               : this(id, title, description, imageSource, minutesToCook, isApproved, user, difficulty, ratings, categories, createdBy, createdDate)
        {
            PreparationMethod = preparationMethod;            
            Ingredients = ingredients;
            Comments = comments;
            Notes = notes;
        }

        public Recipe(int id, string title, string description, PreparationMethod preparationMethod, string imageSource, int minutesToCook, bool isApproved, User user, Difficulty difficulty, List<Rating> ratings, List<Category> categories, List<Ingredient> ingredients, List<Comment> comments, List<Note> notes, string createdBy, DateTime createdDate, string updatedBy, DateTime updatedDate) 
               : this(id, title, description, preparationMethod, imageSource, minutesToCook, isApproved, user, difficulty, ratings, categories, ingredients, comments, notes, createdBy, createdDate)
        { 
            UpdatedBy = updatedBy;
            UpdatedDate = updatedDate;
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
            if (!Regex.IsMatch(value, @"^[a-zA-Z0-9\s\-á]+$"))
            {
                throw new DomainException("Recipe title can only contain letters, numbers, spaces, dashes, and the character 'á'.");
            }
        }
        private void ValidateDescription(string value)
        {
            // Check if instructions are null or empty
            if (string.IsNullOrEmpty(value))
            {
                throw new DomainException("Recipe description cannot be null or empty.");
            }

            // Check if instructions length exceeds maximum allowed characters
            if (value.Length > 1000)
            {
                throw new DomainException("Recipe description cannot exceed 500 characters.");
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

        public int GetRecipeRating()
        {
            double rating = 0;
            double ratingsCounter = 0;

            foreach(Rating r in Ratings)
            {
                ratingsCounter++;
                rating += r.Value;
            }

            rating /= ratingsCounter;

            int rat = (int)rating;

            return rat;
        }

        public int GetRecipeRatingCount()
        {
            int ratingsCounter = 0;

            foreach(Rating r in Ratings)
            {
                ratingsCounter++;
            }

            return ratingsCounter;
        }


    }
}
