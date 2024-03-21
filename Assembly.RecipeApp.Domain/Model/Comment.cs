using Assembly.RecipeApp.Domain.Interfaces;

namespace Assembly.RecipeApp.Domain.Model
{
    public class Comment : AuditableEntity, IEntity
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