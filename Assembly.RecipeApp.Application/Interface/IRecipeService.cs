using Assembly.RecipeApp.Domain.Model;

namespace Assembly.RecipeApp.Application.Interface
{
    public interface IRecipeService : IService<Recipe>
    {
        List<Domain.Model.Recipe> GetFilteredProducts(string name);
    }

}
