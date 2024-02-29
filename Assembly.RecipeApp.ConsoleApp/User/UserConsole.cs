﻿using Assembly.RecipeApp.ConsoleApp.User;
using ConsoleApp;
using System.Runtime.InteropServices;

internal class UserConsole
{
    internal static void Start()
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
                    Create.Start();
                break;

                case "2":
                    Read.Start();
                break;

                case "3":
                    Update.Start();
                break;

                case "4":
                    Delete.Start();
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
}