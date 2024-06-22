using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository.Interfaces;

namespace Assembly.RecipeApp.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService (ICategoryRepository categoryRepository) 
        {
            _categoryRepository = categoryRepository;
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        } // Feito

        public Category GetById(int id)
        {
            return _categoryRepository.GetById(id);
        } // Feito

        public bool Add(Category entity, User adminUser)
        {
            throw new NotImplementedException();
        }

        public bool AddRecipe(int categoryId, int recipeId)
        {
            return _categoryRepository.AddRecipe(categoryId, recipeId);
        }

        public bool RemoveRecipe(int categoryId, int recipeId)
        {
            return _categoryRepository.RemoveRecipe(categoryId, recipeId);
        }

        public bool Update(Category entity, User adminUser)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id, User adminUser)
        {
            throw new NotImplementedException();
        }

    }
}