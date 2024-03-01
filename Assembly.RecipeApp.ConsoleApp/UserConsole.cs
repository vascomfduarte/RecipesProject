using Assembly.RecipeApp.Application.Services;
using Assembly.RecipeApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.RecipeApp.ConsoleApp
{
    public class UserConsole
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
                Console.WriteLine("0. Back");

                Console.Write("\nSelect an option: ");
                string input = Console.ReadLine();

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

                User user = new User(username, password, email, firstName, lastName);

                bool success = _userServices.Add(user);

                if (success)
                {
                    Console.WriteLine("\nUser created successfully");
                    run = false;
                }
                else
                {
                    Console.WriteLine("\nFailed to create user");
                    run = false;
                }

                Console.ReadLine();

            }
        }
        public static void Read()
        {
            UserServices _userServices = new UserServices();

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

                        List<User> users1 = _userServices.GetAll();

                        foreach (User u in users1)
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

                        string choose2 = "";

                        foreach (User u in _userServices.GetAll())
                        {
                            choose2 += $"{u.Id},";
                        }

                        Console.WriteLine($"\nId's: {choose2}");
                        Console.Write($"\nPlease choose one: ");

                        int choice = int.Parse(Console.ReadLine());

                        User user = _userServices.GetById(choice);

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

                        List<User> users3 = _userServices.GetFilteredUsers(term);

                        foreach (User u in users3)
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

                        string choose4 = "";
                        List <User> users4 = _userServices.GetAll();
                        foreach (User u in users4)
                        {
                            if (u.IsBlocked)
                                choose4 += $"{u.Id},";
                        }

                        foreach (User u in users4)
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

                        string choose5 = "";
                        List<User> users5 = _userServices.GetAll();
                        foreach (User u in users5)
                        {
                            if (u.IsAdmin)
                                choose5 += $"{u.Id},";
                        }

                        foreach (User u in users5)
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
        public static void Update()
        {
            UserServices _userServices = new UserServices();

            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.WriteLine("Update User:\n");
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

                User user = new User(username, password, email, firstName, lastName);

                bool success = _userServices.Update(user);

                if (success)
                {
                    Console.WriteLine("\nUser updated successfully");
                    run = false;
                }
                else
                {
                    Console.WriteLine("\nFailed to update user");
                    run = false;
                }

                Console.ReadLine();
            }
        }

        public static void Delete()
        {
            throw new NotImplementedException();
        }

    }
}
