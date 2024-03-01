using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.RecipeApp.ConsoleApp
{
    public static class CategoryConsole
    {
        public static void Start()
        {
            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.WriteLine("Menu: ");
                Console.WriteLine("1. Create");
                Console.WriteLine("2. Read");
                Console.WriteLine("3. Update");
                Console.WriteLine("4. Delete");

                Console.Write("\nSelect an option: ");
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Wrong Input. Please choose again.");
                    Console.ReadLine();
                    continue;
                }

                switch (input.Trim())
                {
                    case "1":
                        Create();
                        break;

                    case "2":
                        Read();
                        break;

                    case "3":
                        Update();
                        break;

                    case "4":
                        Delete();
                        break;

                    case "0":
                        Program.Main();
                        break;

                    default:
                        if (String.IsNullOrEmpty(input))
                        {
                            Console.WriteLine("Wrong Input. Please choose again.");
                            Console.ReadLine();
                            continue;
                        }
                        break;
                }
            }
        }

        public static void Create()
        {
            throw new NotImplementedException();
        }
        public static void Read()
        {
            throw new NotImplementedException();
        }
        public static void Update()
        {
            throw new NotImplementedException();
        }
        public static void Delete()
        {
            throw new NotImplementedException();
        }

    }
}
