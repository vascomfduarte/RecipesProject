using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.RecipeApp.Repository.Repos
{
    public class CommentRepository : ICommentRepository
    {
        private static string _connectionString = ConnectionStringProvider.GetConnectionString();

        public Comment Comment;

        public List<Comment> GetAll()
        {
            throw new NotImplementedException();
        }

        public Comment GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Comment Add(Comment entity)
        {
            throw new NotImplementedException();
        }

        public Comment Delete(Comment entity)
        {
            throw new NotImplementedException();
        }

        public Comment Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Comment Update(Comment entity)
        {
            throw new NotImplementedException();
        }
    }
}
