using Assembly.RecipeApp.Domain.Interfaces;

namespace Assembly.RecipeApp.Domain.Model
{
    public class Comment : AuditableEntity, IEntity
    {
        public int Id { get; set; }
        public string Body { get; set; }

        public Recipe Recipe { get; set; }
        public User User { get; set; }

        public Comment (string body, Recipe recipe, User user)
        {
            Body = body;
            Recipe = recipe;
            User = user;
            CreatedDate = DateTime.Now;
            CreatedBy = User.Username;
        }

        public Comment(int id, string body, Recipe recipe, User user, DateTime createdDate)
        {
            Id = id;
            Body = body;
            Recipe = recipe;
            User = user;
            CreatedDate = createdDate;
        }
        
    }
}