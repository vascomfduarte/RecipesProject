using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository.Repos;
using System.ComponentModel;
using System.Globalization;

namespace Assembly.RecipeApp.Application.Services
{
    public class RecipeServices : IRecipeService
    {
        private static RecipeRepository _recipeRepository = new RecipeRepository();
        private static UserServices _userServices = new UserServices();
        private static IngredientServices _ingredientServices = new IngredientServices();

        public bool Add(Recipe recipe)
        {
            // Validate if Title is already in use
            if (GetAll().Any(r => r.Title == recipe.Title))
                throw new ArgumentException("Title is already in use.", nameof(recipe.Title));

            // Validate image source format
            if (recipe.ImageSource is not null)
            {
                if (!Uri.TryCreate(recipe.ImageSource, UriKind.Absolute, out Uri uriResult) || uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps)
                {
                    recipe.ImageSource = "https://iili.io/JGnl0eS.png";
                    //throw new ArgumentException("Invalid image URL.", nameof(user.ImageSource));
                }
            }
            else if (recipe.ImageSource is null)
            {
                // Set a default to ImageSource
                recipe.ImageSource = "https://iili.io/JGnl0eS.png";
            }

            // Set isBlocked to false by default if not provided
            recipe.SetIsApproved(recipe, false);

            // Format the User object's properties into a string representation
            string recipeString = $"{recipe.Title}|{recipe.Instructions}|{recipe.ImageSource}|{recipe.MinutesToCook}|{(recipe.IsApproved ? "1" : "0")}|{recipe.UserId}|{recipe.DifficultyId}|{recipe.CreatedAt:yyyy-MM-dd}";

            // Call the UserRepository's Add method with the formatted string representation of the User
            return _recipeRepository.Add(recipeString);
        }

        public List<Recipe> GetAll()
        {
            // Retrieve the list of recipe strings from the repository
            List<string> recipeStrings = _recipeRepository.GetAll();

            List<Recipe> returnRecipes = new List<Recipe>();

            foreach (string recipeString in recipeStrings)
            {
                // Parse recipe string to extract recipe data
                Recipe recipe = ParseRecipe(recipeString);

                // Add the parsed recipe to the list
                returnRecipes.Add(recipe);
            }

            return returnRecipes;
        }

        public Recipe GetById(int id)
        {
            // Retrieve the list of recipe parameters through a string from the repository
            string recipeString = _recipeRepository.GetById(id);

            // Call and return recipe builder method
            return ParseRecipe(recipeString);
        }

        public List<Recipe> GetFilteredRecipes(string name)
        {
            // Retrieve the list of recipe strings from the repository
            List<string> recipeStrings = _recipeRepository.GetFilteredRecipes(name);

            if (string.IsNullOrEmpty(name))
                recipeStrings = _recipeRepository.GetAll();

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
            DateTime createdAt = DateTime.ParseExact(recipeData[8], "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            //Add additional data
            List<Ingredient> ingredients = _ingredientServices.GetRecipeIngredients(id);

            // Create a new Recipe object and populate its properties
            Recipe recipe = new Recipe(id,
                                 title,
                                 instructions,
                                 imageSource,
                                 minutesToCook,
                                 isApproved,
                                 userId,
                                 difficultyId,
                                 createdAt,
                                 ingredients);

            //recipe.User = _userServices.GetUserById(recipe.UserId);
            // recipe.Difficulty = DifficultyServices.GetDifficultyById(recipe.DifficultyId);
            // recipe.Categories = CategoryServices.GetCategory(recipe.Id);
            // recipe.Ratings = RatingServices.GetRating(recipe.Id);
            // recipe.Comments = CommentServices.GetComment(recipe.Id);            

            return recipe;
        }
    }
}
