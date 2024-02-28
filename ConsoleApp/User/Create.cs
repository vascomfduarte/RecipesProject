using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.User
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
                Console.WriteLine("New User: ");
                Console.Write("Username: ");
                Console.Write("Password: ");
                Console.Write("Email: ");
                Console.Write("FirstName: ");
                Console.Write("LastName: ");
                Console.Write("ContentBio: -");
                Console.Write("ImageSource: -");
                Console.Write("IsAdmin: -");
                Console.Write("IsBlocked: -");                
            }
        }
    }
}
