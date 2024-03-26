using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository.Interfaces;
using Assembly.RecipeApp.Repository.Repos;

namespace Assembly.RecipeApp.Application.Services
{
    internal class RatingServices : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;

        public RatingServices(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public List<Rating> GetAll()
        {
            return _ratingRepository.GetAll();
        } // Feito 

        public Rating GetById(int id)
        {
            return _ratingRepository.GetById(id);
        } // Feito 

        public bool Add(Rating entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id, User user)
        {
            throw new NotImplementedException();
        }

        public bool Update(Rating entity, User user)
        {
            throw new NotImplementedException();
        }
    }
}
