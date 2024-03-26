using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository.Interfaces;
using Assembly.RecipeApp.Repository.Repos;

namespace Assembly.RecipeApp.Application.Services
{
    public class CommentServices : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentServices(ICommentRepository ingredientRepository)
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

        public bool Add(Comment entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id, User user)
        {
            throw new NotImplementedException();
        }

        public bool Update(Comment entity, User user)
        {
            throw new NotImplementedException();
        }
    }
}
