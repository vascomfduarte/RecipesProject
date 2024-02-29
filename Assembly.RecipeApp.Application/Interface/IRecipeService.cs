using Assembly.Recipe.Domain.Model;

namespace Assembly.RecipeApp.Application.Interface
{
    public interface IRecipeService : IService<Domain.Model.Recipe>
    {
        List<Domain.Model.Recipe> GetFilteredProducts(string name);
    }

}
