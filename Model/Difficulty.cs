﻿
namespace Model
{
    public class Difficulty
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Recipe> Recipes { get; set; }
    }
}