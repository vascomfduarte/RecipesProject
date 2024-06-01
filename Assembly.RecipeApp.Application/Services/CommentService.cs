using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository.Interfaces;
using Assembly.RecipeApp.Repository.Repos;

namespace Assembly.RecipeApp.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository ingredientRepository)
        {
            _commentRepository = ingredientRepository;
        }

        public List<Comment> GetAll()
        {
            return _commentRepository.GetAll();
        } // Feito 

        public Comment GetById(int id)
        {
            return _commentRepository.GetById(id);
        } // Feito

        public List<Comment> GetByRecipeId(int id)
        {
            return _commentRepository.GetByRecipeId(id);
        } // Feito

        public List<Comment> GetByUserId(int id)
        {
            return _commentRepository.GetByUserId(id);
        } // Feito

        public bool Add(Comment entity)
        {
            return _commentRepository.Add(entity);
        }

        public bool Delete(int id, User user)
        {
            Comment c = _commentRepository.GetById(id);

            if (user.IsAdmin || user.Id == c.User.Id) 
            {
               return _commentRepository.Delete(c);
            }

            return false;
        }

        public bool Update(Comment entity, User user)
        {
            if (user.IsAdmin || user.Id == entity.User.Id)
            {
                return _commentRepository.Update(entity);
            }

            return false;
        }
    }
}
