using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository.Interfaces;
using Assembly.RecipeApp.Repository.Repos;

namespace Assembly.RecipeApp.Application.Services
{
    public class DifficultyService : IDifficultyService
    {
        private readonly IDifficultyRepository _difficultyRepository;

        public DifficultyService(IDifficultyRepository difficultyRepository)
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

        public Difficulty GetByName(string name)
        {
            return _difficultyRepository.GetByName(name);
        } // Feito

        public bool Add(Difficulty entity, User currentUser)
        {
            if (currentUser.IsAdmin)
            {
                // Validate if Ingredient with the same name already exists
                if (GetAll().Any(i => i.Name == entity.Name))
                    throw new ArgumentException("A difficulty with the same name already exists.", nameof(entity.Name));

                _difficultyRepository.Add(entity);
                return true;
            }

            return false;
        } // Feito 

        public bool Update(Difficulty entity, User currentUser)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Difficulty entity, User currentUser)
        {
            if (currentUser.IsAdmin)
            {
                _difficultyRepository.Delete(entity);
                return true;
            }

            return false;
        } // Feito 
    }
}
