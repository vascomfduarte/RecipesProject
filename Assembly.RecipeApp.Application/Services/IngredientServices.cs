﻿using Model;
using Repository;

namespace Assembly.RecipeApp.Application.Services
{
    public class IngredientServices
    {
        private static IngredientRepository _ingredientRepository = new IngredientRepository();

        internal List<string> GetIngredient(int recipeId)
        {
            throw new NotImplementedException();
        }
    }
}