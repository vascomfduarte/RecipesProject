using Repository;
using Model;

namespace Assembly.Recipe.Application.Services
{
    public class RecipeServices
    {
        private static RecipeRepository _recipeRepository = new RecipeRepository();
        private static UserServices _userServices = new UserServices();
        private static RecipeIngredientsServices _recipeIngredientsServices = new RecipeIngredientsServices();

        public static Recipe BuildRecipe(string recipeString)
        {
            // Split the recipe string into individual parameters
            string[] parameters = recipeString.Split('|');

            // Set a default image
            string imageUrl = string.IsNullOrEmpty(parameters[3]) ? "https://iili.io/JGnl0eS.png" : parameters[3];

            // Convert integer representation of IsApproved to boolean
            bool isApproved = parameters[6] == "1";

            // Create a new Recipe object and populate its properties
            Recipe recipe = new Recipe
            {
                Id = int.Parse(parameters[0]),
                Title = parameters[1],
                Instructions = parameters[2],
                ImageSource = imageUrl,
                MinutesToCook = int.Parse(parameters[4]),
                IsApproved = isApproved,
                UserId = int.Parse(parameters[6]),
                DifficultyId = int.Parse(parameters[7]),
                CreatedAt = parameters[8],
            };

            recipe.User = _userServices.GetUserById(recipe.UserId);
            // recipe.Difficulty = DifficultyServices.GetDifficultyById(recipe.DifficultyId);
            // recipe.Categories = CategoryServices.GetCategory(recipe.Id);
            // recipe.Ratings = RatingServices.GetRating(recipe.Id);
            // recipe.Comments = CommentServices.GetComment(recipe.Id);
            //recipe.RecipeIngredients = _recipeIngredientsServices.GetRecipeIngredients(recipe.Id);

            return recipe;
        }

        public List<Recipe> GetRecipesByName(string title)
        {
            if (string.IsNullOrEmpty(title))
                title = "";

            // Retrieve the list of recipe strings from the repository
            List<string> recipeStrings = _recipeRepository.GetRecipesByName(title);

            List<Recipe> returnRecipes = new List<Recipe>();
            Recipe recipe = new Recipe();

            foreach (string strg in recipeStrings)
            {
                // Call Recipe builder method and assign it 
                recipe = BuildRecipe(strg);

                // Check if the constructed Recipe object is approved and add it to the returnlist
                if (true) //recipe.IsApproved           
                    returnRecipes.Add(recipe);
            }

            return returnRecipes;
        }

        public Recipe GetRecipeById(int recipeId)
        {
            // Retrieve the list of recipe parameters through a string from the repository
            string recipeString = _recipeRepository.GetRecipeById(recipeId);

            // Call and return Recipe builder method
            return BuildRecipe(recipeString);
        }

    }
}

