using System;
using System.Collections.Generic;

namespace Assembly.Recipe.Domain.Model
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Instructions { get; set; }
        public string ImageSource { get; set; }
        public int MinutesToCook { get; set; }
        public bool IsApproved { get; set; }
        public int UserId { get; set; }
        public int DifficultyId { get; set; }
        public string CreatedAt { get; set; }

        public User User { get; set; }
        public Difficulty Difficulty { get; set; }
        public List<Category> Categories { get; set; }
        public List<Rating> Ratings { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<RecipeIngredients> RecipeIngredients { get; set; }
        //public List<RecipeCategory> RecipeCategories { get; set; }
    }

}