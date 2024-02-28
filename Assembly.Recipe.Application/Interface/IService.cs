using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Recipe.Application.Interface
{
    public interface IService<T>
    {
        List<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }

    public interface IUserService : IService<User>
    { }

    public interface IRecipeService : IService<Recipe>
    {
        GetRecipeByName();
    }

}
