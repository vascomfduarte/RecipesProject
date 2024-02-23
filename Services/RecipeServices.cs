using Repository;
using Model;
using static System.Net.WebRequestMethods;

namespace Services
{
    public class RecipeServices
    {
        private static RecipeRepository _recipeRepository = new RecipeRepository();

        public List<string> GetRecipes()
        {
			// Collect data from repository
			return _recipeRepository.GetRecipes();
        }

        public List<Recipe> GetRecipesByName(string title)
        {
            List<Recipe> returnRecipes = new List<Recipe>();

            // Retrieve the list of recipe strings from the repository
            List<string> recipeStrings = _recipeRepository.GetRecipesByName(title.ToLower().Trim());

            // Process each recipe string
            foreach (string recipeString in recipeStrings)
            {
                // Split the recipe string into individual parameters
                string[] parameters = recipeString.Split('|');

                // Convert integer representation of IsAproved to boolean
                bool isApproved = parameters[6] == "1";

                // Define a default image
                string imageUrl = String.IsNullOrEmpty(parameters[3]) ? "https://iili.io/JGnl0eS.png" : parameters[3];

                // Create a new Recipe object and populate its properties
                Recipe recipe = new Recipe
                {
                    Id = int.Parse(parameters[0]),
                    Title = parameters[1],
                    // Assuming Author and Ingredients are not handled in the string
                    // You may need additional logic to populate these properties
                    Instructions = parameters[2],
                    ImageSource = imageUrl,
                    MinutesToCook = int.Parse(parameters[4]),
                    UserId = int.Parse(parameters[5]),
                    // Assuming enums are not handled in the string
                    // You may need additional logic to parse enums from strings
                    IsAproved = isApproved
                };

                // Add the constructed Recipe object to the list
                returnRecipes.Add(recipe);
            }

            return returnRecipes;

        }
    }
}

//< div class="col-lg-3 col-md-4 col-sm-6 mb-4">
//    < div class= "card h-100" >
//        < img src = "@recipe.ImageSource" class= "card-img-top" alt = "@recipe.Title" style = "max-height: 300px; object-fit: cover;" >
//        < div class= "card-body" >
//            < h5 class= "card-title" > @recipe.Title </ h5 >
//            < p class= "card-text" > @recipe.Instructions </ p >
//        </ div >
//        < ul class= "list-group list-group-flush" >
//            < li class= "list-group-item" >< strong > Minutes to Cook:</ strong > @recipe.MinutesToCook </ li >
//            < li class= "list-group-item" >< strong > Approved:</ strong > @(recipe.IsAproved ? "Yes" : "No") </ li >
//            < li class= "list-group-item" >< strong > User ID:</ strong > @recipe.UserId </ li >
//        </ ul >
//    </ div >
//</ div >