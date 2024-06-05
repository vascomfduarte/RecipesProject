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
    public class PreparationMethodService : IPreparationMethodService
    {
        private readonly IPreparationMethodRepository _preparationMethodRepository;

        public PreparationMethodService(IPreparationMethodRepository preparationMethodRepository)
        {
            _preparationMethodRepository = preparationMethodRepository;
        }

        public List<PreparationMethod> GetAll()
        {
            throw new NotImplementedException();
        }

        public PreparationMethod GetById(int id)
        {
            throw new NotImplementedException();
        }

        public PreparationMethod GetByRecipeId(int recipeId)
        {
            return _preparationMethodRepository.GetByRecipeId(recipeId);
        } // Feito

        public List<PreparationStep> GetStepsByRecipeId(int recipeId)
        {
            return _preparationMethodRepository.GetStepsByRecipeId(recipeId);
        } // Feito        

        public PreparationStep GetStepById(int stepId)
        {
            return _preparationMethodRepository.GetStepById(stepId);
        } // Feito      

        public bool Add(PreparationMethod entity)
        {
            throw new NotImplementedException();
        }

        public bool AddStep(PreparationStep entity, int recipeId)
        {
            return _preparationMethodRepository.AddStep(entity, recipeId);
        } // Feito

        public bool Add(PreparationMethod entity, int recipeId)
        {
            return _preparationMethodRepository.Add(entity, recipeId);
        } // Feito

        public bool Update(PreparationMethod entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(PreparationMethod entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteStep(PreparationStep entity)
        {
            return _preparationMethodRepository.DeleteStep(entity);
        } // Feito

    }
}
