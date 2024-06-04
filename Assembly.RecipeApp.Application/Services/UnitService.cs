using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository.Interfaces;
using Assembly.RecipeApp.Repository.Repos;

namespace Assembly.RecipeApp.Application.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepository;

        public UnitService(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public List<Unit> GetAll()
        {
            return _unitRepository.GetAll();
        } // Feito

        public Unit GetById(int unitId)
        {
            return _unitRepository.GetById(unitId);
        } // Feito

        public bool Add(Unit entity, User user)
        {
            if (user.IsAdmin)
            {
                // Validate if Ingredient with the same name already exists
                if (GetAll().Any(i => i.Name == entity.Name))
                    throw new ArgumentException("A Unit with the same name already exists.", nameof(entity.Name));

                _unitRepository.Add(entity, user);
                return true;
            }

            return false;
        } // Feito 

        public bool Update(Unit entity, User user)
        {
            return _unitRepository.Update(entity, user);
        } // Feito

        public bool Delete(int id, User user)
        {
            return _unitRepository.Delete(id, user);
        } // Feito

    }
}
