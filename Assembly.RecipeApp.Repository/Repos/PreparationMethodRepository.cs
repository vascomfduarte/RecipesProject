using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.RecipeApp.Repository.Repos
{
    public class PreparationMethodRepository : IPreparationMethodRepository
    {
        private static string _connectionString = ConnectionStringProvider.GetConnectionString();

        public List<PreparationMethod> GetAll()
        {
            throw new NotImplementedException();
        }

        public PreparationMethod GetById(int id)
        {
            throw new NotImplementedException();
        }

        public PreparationMethod Add(PreparationMethod entity)
        {
            throw new NotImplementedException();
        }

        public PreparationMethod Delete(PreparationMethod entity)
        {
            throw new NotImplementedException();
        }

        public PreparationMethod Delete(int id)
        {
            throw new NotImplementedException();
        }

        public PreparationMethod Update(PreparationMethod entity)
        {
            throw new NotImplementedException();
        }
    }
}
