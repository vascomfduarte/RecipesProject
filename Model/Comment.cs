
using Model;

namespace RecipesWebApp.Model.Model
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int RecipeId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public Recipe Recipe { get; set; }
        public User User { get; set; }
    }
}