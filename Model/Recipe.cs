using System;
using System.Collections.Generic;

namespace Model
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }        
        public List<Ingredient> Ingredients { get; set; }
        public string Instructions { get; set; }
        public string ImageSource { get; set; }
        public int MinutesToCook { get; set; }
        public bool IsAproved { get; set; }
        public int UserId { get; set; }
        public enum Ratings
        {
            One = 1,
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
        }
        public enum Category
        {
            Breakfast,
            Lunch,
            Dinner,
            Appetizer,
            Salad,
            MainCourse,
            SideDish,
            Dessert,
            Snack,
            Soup,
            Holiday,
            Vegetarian
        }
        public enum Difficulty
        {
            Easy,
            Medium,
            Hard
        }

    }
}