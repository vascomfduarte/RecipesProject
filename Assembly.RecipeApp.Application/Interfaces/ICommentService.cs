using Assembly.RecipeApp.Domain.Model;

namespace Assembly.RecipeApp.Application.Interfaces
{
    public interface ICommentService
    {
        List<Comment> GetAll();
        Comment GetById(int id);
        bool Add(Comment entity);
        bool Update(Comment entity, User user);
        bool Delete(int id, User user);
    }
}