using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.RecipeApp.Application.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public List<Note> GetAll()
        {
            return _noteRepository.GetAll();
        } // Feito 

        public Note GetById(int id)
        {
            return _noteRepository.GetById(id);
        } // Feito

        public List<Note> GetByRecipeId(int id)
        {
            return _noteRepository.GetByRecipeId(id);
        } // Feito

        public List<Note> GetByUserId(int id)
        {
            return _noteRepository.GetByUserId(id);
        } // Feito

        public bool Add(Note entity)
        {
            return _noteRepository.Add(entity);
        } // Feito

        public bool Delete(int id, User user)
        {
            Note n = _noteRepository.GetById(id);

            if (user.IsAdmin || user.Id == n.User.Id)
            {
                return _noteRepository.Delete(n);
            }

            return false;
        } // Feito

        public bool DeleteByRecipeId(int recipeId)
        {
            return _noteRepository.DeleteByRecipeId(recipeId);
        } // Feito

        public bool Update(Note entity, User user)
        {
            if (user.IsAdmin || user.Id == entity.User.Id)
            {
                return _noteRepository.Update(entity);
            }

            return false;
        } // Feito
    }
}
