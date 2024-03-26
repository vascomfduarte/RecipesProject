using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository.Interfaces;
using Assembly.RecipeApp.Repository.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.RecipeApp.Application.Services
{
    public class PreparationMethodServices : IPreparationMethodService
    {
        private readonly IPreparationMethodRepository _preparationMethodRepository;

        public PreparationMethodServices(IPreparationMethodRepository preparationMethodRepository)
        {
            _preparationMethodRepository = preparationMethodRepository;
        }

        public List<PreparationMethod> GetAll()
        {
            return _preparationMethodRepository.GetAll();
        } // Feito 

        public PreparationMethod GetById(int id)
        {
            return _preparationMethodRepository.GetById(id);
        } // Feito 

        public bool Add(PreparationMethod entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(PreparationMethod entity)
        {
            throw new NotImplementedException();
        }
    }
}
