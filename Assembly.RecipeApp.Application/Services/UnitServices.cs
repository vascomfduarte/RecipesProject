using Assembly.RecipeApp.Application.Interface;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository;

namespace Assembly.RecipeApp.Application.Services
{
    internal class UnitServices : IUnitService
    {
        private static UnitRepository _unitRepository = new UnitRepository();

        public bool Add(Unit entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Unit> GetAll()
        {
            throw new NotImplementedException();
        }

        public Unit GetById(int unitId)
        {
            // Retrieve the list of ingredient parameters through a string from the repository
            string unitString = _unitRepository.GetById(unitId);

            // Split the ingredient string into individual parameters
            string[] unitData = unitString.Split('|');

            // Extract individual data
            int id = int.Parse(unitData[0]);
            string name = unitData[1];

            // Create a new Unit object and populate its properties
            Unit unit = new Unit(id, name);

            return unit;
        }

        public bool Update(Unit entity)
        {
            throw new NotImplementedException();
        }
    }
}
