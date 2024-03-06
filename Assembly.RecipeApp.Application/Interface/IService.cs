using Assembly.RecipeApp.Domain.Model;

namespace Assembly.RecipeApp.Application.Interface
{
    public interface IService<T>
    {
        List<T> GetAll();
        T GetById(int id);
        bool Add(T entity, User user);
        bool Update(T entity, User user);
        bool Delete(int id, User user);
    }
}

