using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.RecipeApp.Repository.Repos
{
    public class RatingRepository : IRatingRepository
    {
        public bool Add(Rating entity, User adminUser)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id, User adminUser)
        {
            throw new NotImplementedException();
        }

        public List<Rating> GetAll()
        {
            throw new NotImplementedException();
        }

        public Rating GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Rating> GetByRecipeId(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Rating entity, User adminUser)
        {
            throw new NotImplementedException();
        }
    }
}
