using Assembly.Recipe.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.RecipeApp.ConsoleApp.User
{
    internal class Create
    {
        internal static void Start()
        {
            UserServices _userServices = new UserServices();

            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.WriteLine("New User:\n");
                Console.Write("Username: ");
                string username = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();
                Console.Write("Email: ");
                string email = Console.ReadLine();
                Console.Write("FirstName: ");
                string firstName = Console.ReadLine();
                Console.Write("LastName: ");
                string lastName = Console.ReadLine();
                //Console.Write("ContentBio: -");
                //string username = Console.ReadLine();
                //Console.Write("ImageSource: -");
                //string username = Console.ReadLine();
                //Console.Write("IsAdmin: -");
                //string username = Console.ReadLine();
                //Console.Write("IsBlocked: -");

                (bool success, string message) = _userServices.CreateUser(username, password, email, firstName, lastName);

                if (success)
                {
                    Console.WriteLine("\nUser created successfully");
                    run = false;
                }
                else
                {
                    Console.WriteLine("\nFailed to create user: " + message);
                    run = false;
                }

                Console.ReadLine();

            }
        }
    }
}
