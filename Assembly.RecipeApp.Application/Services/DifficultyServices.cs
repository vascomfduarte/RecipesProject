using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository.Interfaces;

namespace Assembly.RecipeApp.Application.Services
{
    public class DifficultyServices : IDifficultyService
    {
        private readonly IDifficultyRepository _difficultyRepository;

        public DifficultyServices(IDifficultyRepository difficultyRepository)
        {
            _difficultyRepository = difficultyRepository;
        }

        public List<Difficulty> GetAll()
        {
            return _difficultyRepository.GetAll();
        } // Feito

        public Difficulty GetById(int id)
        {
            return _difficultyRepository.GetById(id);
        } // Feito

        public bool Add(Difficulty entity, User adminUser)
        {
            throw new NotImplementedException();
        }

        public bool Update(Difficulty entity, User adminUser)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id, User adminUser)
        {
            throw new NotImplementedException();
        }
    }
}
