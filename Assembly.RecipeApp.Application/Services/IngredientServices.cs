using Assembly.RecipeApp.Application.Interface;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository;
using System.Globalization;

namespace Assembly.RecipeApp.Application.Services
{
    public class IngredientServices : IIngredientService
    {
        private static IngredientRepository _ingredientRepository = new IngredientRepository();
        private static UnitServices _unitServices = new UnitServices();

        public bool Add(Ingredient ingredient, User currentUser)
        {
            string ingredientString = "";

            if (currentUser.IsAdmin)
            {
                // Validate if Ingredient with the same name already exists
                if (GetAll().Any(i => i.Name == ingredient.Name))
                    throw new ArgumentException("An ingredient with the same name already exists.", nameof(ingredient.Name));

                // Format the ingredient's properties into a string representation
                ingredientString = $"{ingredient.Name}";
            }

            // Add the ingredient to the repository or database
            return _ingredientRepository.Add(ingredientString);
        }

        public List<Ingredient> GetAll()
        {
            // Retrieve the list of ingredient strings from the repository
            List<string> ingredientStrings = _ingredientRepository.GetAll();

            List<Ingredient> returnIngredients = new List<Ingredient>();

            foreach (string ingredientString in ingredientStrings)
            {
                // Parse ingredient string to extract ingredient data
                Ingredient ingredient = ParseIngredient(ingredientString);

                // Add the parsed ingredient to the list
                returnIngredients.Add(ingredient);
            }

            return returnIngredients;
        }

        public Ingredient GetById(int id)
        {
            // Retrieve the list of ingredient parameters through a string from the repository
            string ingredientString = _ingredientRepository.GetById(id);

            // Call and return ingredient builder method
            return ParseIngredient(ingredientString);
        }

        public List<Ingredient> GetRecipeIngredients(int recipeId)
        {
            // Retrieve the list of recipe strings from the repository
            List<string> ingredientStrings = _ingredientRepository.GetRecipeIngredients(recipeId);

            List<Ingredient> returnIngredients = new List<Ingredient>();

            foreach (string ingredientString in ingredientStrings)
            {
                Ingredient ingredient = ParseIngredient(ingredientString);

                returnIngredients.Add(ingredient);
            }

            return returnIngredients;
        }

        public bool Update(Ingredient entity, User adminUser)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id, User adminUser)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method associated with all the get methods from Ingredient.
        /// It is responsible for receiving a string, creating an instance of Ingredient with the parameters contained in the string and returning it.
        /// </summary>
        /// <param ingredientString="parameter"></param>
        /// <returns></returns>
        public static Ingredient ParseIngredient(string ingredientString)
        {
            // Split the ingredient string into individual parameters
            string[] ingredientData = ingredientString.Split('|');

            // Extract individual data
            int id = int.Parse(ingredientData[0]);
            string name = ingredientData[1];
            int amount = int.Parse(ingredientData[2]);
            int unitId = int.Parse(ingredientData[3]);

            //Add additional data
            Unit unit = _unitServices.GetById(unitId);

            // Create a new Ingredient object and populate its properties
            Ingredient ingredient = new Ingredient(id, name, amount, unit);

            return ingredient;
        }

    }
}
