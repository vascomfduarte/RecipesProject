using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository.Interfaces;
using Assembly.RecipeApp.Repository.Repos;

namespace Assembly.RecipeApp.Application.Services
{
    internal class UnitServices : IUnitService
    {
        private readonly IUnitRepository _unitRepository;

        public UnitServices(IUnitRepository unitRepository)
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

        public bool Add(Unit entity, User adminUser)
        {
            throw new NotImplementedException();
        }

        public bool Update(Unit entity, User adminUser)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id, User adminUser)
        {
            throw new NotImplementedException();
        }

    }
}
