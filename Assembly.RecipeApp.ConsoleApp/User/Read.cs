using Assembly.Recipe.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;
using Microsoft.VisualBasic.FileIO;
using Assembly.Recipe.Domain.Model;
using Assembly.Recipe.Application.Services;

namespace Assembly.RecipeApp.ConsoleApp.User
{
    internal class Read
    {
        internal static void Start()
        {
            UserServices _userServices = new UserServices();
            Assembly.Recipe.Domain.Model.User user = new Assembly.Recipe.Domain.Model.User();
            List<Model.User> users = new List<Model.User>();

            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.WriteLine("Menu: ");
                Console.WriteLine("1. All Users");
                Console.WriteLine("2. Get User by id");
                Console.WriteLine("3. Get User by name");
                Console.WriteLine("4. Get Blocked Users");
                Console.WriteLine("5. Get Admin Users");
                Console.WriteLine("0. Back");

                Console.Write("\nSelect an option: ");
                string input = Console.ReadLine();

                switch (input.Trim())
                {
                    case "1":

                        users = _userServices.GetAll();

                        foreach (Assembly.Recipe.Domain.Model.User u in users)
                        {
                            Console.WriteLine($"\nUsername: {u.Username}");
                            Console.WriteLine($"Email: {u.Email}");
                            Console.WriteLine($"Name: {u.FirstName} {u.LastName}");
                            Console.WriteLine($"Bio: {u.ContentBio}");
                            Console.WriteLine($"Admin: {u.IsAdmin}");
                            Console.WriteLine($"Blocked: {u.IsBlocked}");
                        }

                        break;

                    case "2":

                        string choose = "";

                        foreach (Assembly.Recipe.Domain.Model.User u in _userServices.GetAll())
                        {
                            choose += $"{u.Id},";
                        }

                        Console.WriteLine($"\nId's: {choose}");
                        Console.Write($"\nPlease choose one: ");

                        int choice = int.Parse(Console.ReadLine());

                        user = _userServices.GetUserById(choice);

                        Console.WriteLine($"\nUsername: {user.Username}");
                        Console.WriteLine($"Email: {user.Email}");
                        Console.WriteLine($"Name: {user.FirstName} {user.LastName}");
                        Console.WriteLine($"Bio: {user.ContentBio}");
                        Console.WriteLine($"Admin: {user.IsAdmin}");
                        Console.WriteLine($"Blocked: {user.IsBlocked}");

                        break;

                    case "3":

                        Console.WriteLine("\n\nSearch by name: ");
                        Console.Write("\nEnter search term:  ");
                        string term = Console.ReadLine();

                        Console.WriteLine("\n\nSearch Results: ");

                        users = _userServices.GetUserByName(term);

                        foreach (Model.User u in users)
                        {
                            Console.WriteLine($"\nUsername: {u.Username}");
                            Console.WriteLine($"Email: {u.Email}");
                            Console.WriteLine($"Name: {u.FirstName} {u.LastName}");
                            Console.WriteLine($"Bio: {u.ContentBio}");
                            Console.WriteLine($"Admin: {u.IsAdmin}");
                            Console.WriteLine($"Blocked: {u.IsBlocked}");
                        }

                        break;

                    case "4":

                        string option = "blocked";
                        users = _userServices.GetUserByStatus(option);

                        foreach (Model.User u in users)
                        {
                            Console.WriteLine($"\nUsername: {u.Username}");
                            Console.WriteLine($"Email: {u.Email}");
                            Console.WriteLine($"Name: {u.FirstName} {u.LastName}");
                            Console.WriteLine($"Bio: {u.ContentBio}");
                            Console.WriteLine($"Admin: {u.IsAdmin}");
                            Console.WriteLine($"Blocked: {u.IsBlocked}");
                        }

                        break;

                    case "5":

                        string option2 = "admin";
                        users = _userServices.GetUserByStatus(option2);

                        foreach (Model.User u in users)
                        {
                            Console.WriteLine($"\nUsername: {u.Username}");
                            Console.WriteLine($"Email: {u.Email}");
                            Console.WriteLine($"Name: {u.FirstName} {u.LastName}");
                            Console.WriteLine($"Bio: {u.ContentBio}");
                            Console.WriteLine($"Admin: {u.IsAdmin}");
                            Console.WriteLine($"Blocked: {u.IsBlocked}");
                        }

                        break;

                    case "0":
                        UserConsole.Start();
                        break;
                }

                Console.Write("\nPress 'Enter' to continue ");
                Console.ReadLine();
            }
        }

    }
}
