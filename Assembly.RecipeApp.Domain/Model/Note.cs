using Assembly.RecipeApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.RecipeApp.Domain.Model
{
    public class Note : AuditableEntity, IEntity
    {
        public int Id { get; set; }
        public string Body { get; set; }

        public Recipe Recipe { get; set; }
        public User User { get; set; }

        public Note(string body, Recipe recipe, User user)
        {
            Body = body;
            Recipe = recipe;
            User = user;
            CreatedDate = DateTime.Now;
            CreatedBy = User.Username;
        }

        public Note(int id, string body, Recipe recipe, User user, DateTime createdDate)
        {
            Id = id;
            Body = body;
            Recipe = recipe;
            User = user;
            CreatedDate = createdDate;
        }

    }
}
