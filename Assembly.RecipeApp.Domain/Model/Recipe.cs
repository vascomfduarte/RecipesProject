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
        public int MinutesToCook { get; set; }
        public bool IsApproved { get; private set; }
        public int UserId { get; private set; }
        public int DifficultyId { get; set; }
        public string CreatedAt { get; private set; }

        public User User { get; set; }
        public Difficulty Difficulty { get; set; }
        public List<Category> Categories { get; set; }
        public List<Rating> Ratings { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        //public List<RecipeIngredients> RecipeIngredients { get; set; }
        //public List<RecipeCategory> RecipeCategories { get; set; }

        public Recipe (string title, string instructions, int minutesToCook, int userId, int difficultyId, string createdAt)
        {
            Title = title;
            Instructions = instructions;
            MinutesToCook = minutesToCook;
            UserId = userId;
            DifficultyId = difficultyId;
            //CreatedAt = DateTime.UtcNow();
        }

        public Recipe(int id, string title, string instructions, int minutesToCook, int userId, int difficultyId, string createdAt) 
            : this(title, instructions, minutesToCook, userId, difficultyId, createdAt)
        {
            Id = id;            
        }

        public Recipe(int id, string title, string instructions, string imageSource, int minutesToCook, bool isApproved, int userId, int difficultyId, string createdAt) 
            : this (id, title, instructions, minutesToCook, userId, difficultyId, createdAt)
        {
            ImageSource = imageSource;
            IsApproved = isApproved;
        }
    }

}
