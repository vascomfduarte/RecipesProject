using Assembly.RecipeApp.Domain.Model;

public class Program
{
    public static void Main()
    {
        bool run = true;
        while (run)
        {
            Console.Clear();
            Console.WriteLine("CRUD Test Zone:");
            Console.WriteLine("1. Category");
            Console.WriteLine("2. Comment");
            Console.WriteLine("3. Difficulty");
            Console.WriteLine("4. Ingredient");
            Console.WriteLine("5. Rating");
            Console.WriteLine("6. Recipe");
            Console.WriteLine("7. Unit");
            Console.WriteLine("8. User");
            Console.WriteLine("0. Exit");

            Console.Write("\nSelect an option: ");

            string input = Console.ReadLine();

            switch (input.Trim())
            {
                case "1":
                    Assembly.RecipeApp.ConsoleApp.CategoryConsole.Start();
                    break;

                case "2":

                    break;

                case "3":

                    break;

                case "4":
                    Assembly.RecipeApp.ConsoleApp.IngredientConsole.Start();
                    break;

                case "5":

                    break;

                case "6":

                    break;

                case "7":
                    Assembly.RecipeApp.ConsoleApp.UnitConsole.Start();
                    break;

                case "8":
                    Assembly.RecipeApp.ConsoleApp.UserConsole.Start();
                    break;

                case "0":
                    run = false;
                    break;

                default:

                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine("Wrong Input. Please choose again.");
                        Console.ReadLine();
                        continue;
                    }

                    break;
            }

        }
    }
}