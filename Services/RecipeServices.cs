using Repository;
using Model;

namespace Services
{
    public class RecipeServices
    {
        private static RecipeRepository _recipeRepository = new RecipeRepository();
        private static UserServices _userServices = new UserServices();
        private static RecipeIngredientsServices _recipeIngredientsServices = new RecipeIngredientsServices();

        public static List<Recipe> BuildRecipe(List<string> recipeStrings)
        {
            List<Recipe> returnRecipes = new List<Recipe>();

            // Process each recipe string
            foreach (string recipeProperty in recipeStrings)
            {
                // Split the recipe string into individual parameters
                string[] parameters = recipeProperty.Split('|');

                // Convert integer representation of IsApproved to boolean
                bool isApproved = parameters[6] == "1";

                // Define a default image
                string imageUrl = String.IsNullOrEmpty(parameters[3]) ? "https://iili.io/JGnl0eS.png" : parameters[3];

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

                if (true) //recipe.IsApproved
                {
                    // Check if the constructed Recipe object is approved and add it to the returnlist
                    returnRecipes.Add(recipe);
                }   
            }
            return returnRecipes;
        }

        public List<Recipe> GetRecipesByName(string title)
        {
            // Retrieve the list of recipe strings from the repository
            List<string> recipeStrings = _recipeRepository.GetRecipesByName(title.ToLower().Trim());

            // Call and return Recipe builder method
            return BuildRecipe(recipeStrings);
        }
        
        public List<Recipe> GetRecipesById(int recipeId)
        {
            // Retrieve the list of recipe strings from the repository
            List<string> recipeStrings = _recipeRepository.GetRecipesById(recipeId);

            // Call and return Recipe builder method
            return BuildRecipe(recipeStrings);
        }
    }
}

