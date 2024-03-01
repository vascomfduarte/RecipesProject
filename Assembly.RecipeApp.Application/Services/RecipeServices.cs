using Assembly.RecipeApp.Application.Interface;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository;
using System.ComponentModel;

namespace Assembly.RecipeApp.Application.Services
{
    public class RecipeServices : IRecipeService
    {
        private static RecipeRepository _recipeRepository = new RecipeRepository();
        private static UserServices _userServices = new UserServices();
        //private static RecipeIngredientsServices _recipeIngredientsServices = new RecipeIngredientsServices();

        public List<Recipe> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Recipe> GetFilteredProducts(string name)
        {
            if (string.IsNullOrEmpty(name))
                name = "";

            // Retrieve the list of recipe strings from the repository
            List<string> recipeStrings = _recipeRepository.GetRecipesByName(name);

            List<Recipe> returnRecipes = new List<Recipe>();

            foreach (string recipeString in recipeStrings)
            {
                // Call Recipe builder method and assign it 
                Recipe recipe = ParseRecipe(recipeString);

                // Check if the constructed Recipe object is approved and add it to the returnlist
                if (true) //recipe.IsApproved
                    returnRecipes.Add(recipe);
            }

            return returnRecipes;
        }

        public Recipe GetById(int id)
        {
            // Retrieve the list of recipe parameters through a string from the repository
            string recipeString = _recipeRepository.GetRecipeById(id);

            // Call and return Recipe builder method
            return ParseRecipe(recipeString);
        }

        public bool Add(Recipe entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Recipe entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method associated with all the get methods from Recipe. 
        /// It is responsible for receiving a string, creating an instance of Recipe with the parameters contained in the string and returning it.
        /// </summary>
        /// <param recipeString="parameter"></param>
        /// <returns></returns>
        public static Recipe ParseRecipe(string recipeString)
        {
            // Split the recipe string into individual parameters
            string[] recipeData = recipeString.Split('|');

            // Extract individual data
            int id = int.Parse(recipeData[0]);
            string title = recipeData[1];
            string instructions = recipeData[2];            
            string imageSource = string.IsNullOrEmpty(recipeData[3]) ? "https://iili.io/JGnl0eS.png" : recipeData[3]; // Define a default image
            int minutesToCook = int.Parse(recipeData[4]);            
            bool isApproved = recipeData[5] == "1"; // Convert integer representation to boolean
            int userId = int.Parse(recipeData[6]);
            int difficultyId = int.Parse(recipeData[7]);
            string createdAt = recipeData[8];

            // Create a new Recipe object and populate its properties
            Recipe recipe = new Recipe(id,
                                 title,
                                 instructions,
                                 imageSource,
                                 minutesToCook,
                                 isApproved,
                                 userId,
                                 difficultyId,
                                 createdAt);

            //recipe.User = _userServices.GetUserById(recipe.UserId);
            // recipe.Difficulty = DifficultyServices.GetDifficultyById(recipe.DifficultyId);
            // recipe.Categories = CategoryServices.GetCategory(recipe.Id);
            // recipe.Ratings = RatingServices.GetRating(recipe.Id);
            // recipe.Comments = CommentServices.GetComment(recipe.Id);
            //recipe.RecipeIngredients = _recipeIngredientsServices.GetRecipeIngredients(recipe.Id);

            return recipe;
        }

    }
}
