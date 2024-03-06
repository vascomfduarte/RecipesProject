namespace Assembly.RecipeApp.Application.Interfaces
{
    public interface IService<T>
    {
        List<T> GetAll();
        T GetById(int id);
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(int id);
    }
}

