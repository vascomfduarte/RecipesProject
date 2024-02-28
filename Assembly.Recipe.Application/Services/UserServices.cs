using Assembly.Recipe.Application.Interface;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static System.Net.WebRequestMethods;

namespace Assembly.Recipe.Application.Services
{
    public class UserServices : IUserService
    {

        private static UserRepository _userRepository = new UserRepository();

        public (bool success, string errorMessage) CreateUser(string username,
                               string password,
                               string email,
                               string firstName,
                               string lastName,
                               string contentBio = "Let others know who you are",
                               string imageSource = "https://i.ibb.co/ZKV9y5r/da7ed7b0-5f66-4f97-a610-51100d3b9fd2.jpg",
                               string isAdmin = "0",
                               string isBlocked = "0")
        {
            // Validate username
            if (string.IsNullOrWhiteSpace(username) || GetAllUsers().Any(u => u.Username == username))
                return (false, "Invalid username or username already exists.");

            // Validate password strength (Example: Minimum 8 characters with at least one letter and one digit)
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8 || !password.Any(char.IsLetter) || !password.Any(char.IsDigit))
                return (false, "Invalid password. Password must be at least 8 characters long and contain at least one letter and one digit.");

            // Validate email format
            if (string.IsNullOrWhiteSpace(email) ||
                !Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$") ||
                GetAllUsers().Any(u => u.Email == email))
                return (false, "Invalid email address or email address already exists.");

            // Validate first name and last name format
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                return (false, "First name and last name are required.");

            // Validate image source format
            if (!Uri.TryCreate(imageSource, UriKind.Absolute, out Uri uriResult) || uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps)
                return (false, "Invalid image URL.");

            // Set isAdmin to 0 by default if not provided
            if (isAdmin != "1" && isAdmin != "0")
                isAdmin = "0";

            // Call repository method to create user
            bool success = _userRepository.CreateUser(username, password, email, firstName, lastName, contentBio, imageSource, isAdmin, isBlocked);
            return (success, success ? "" : "Failed to create user. Please try again later.");
        }

        public bool UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        //public bool BlockeUser(User adminUser, User user)
        //{            
        //    if (adminUser.isAdmin)
        //    {
        //        bool success = _userRepository.BlockUser(User adminUser, User user);
        //    }
        //    else
        //        return false;

        //    return success; 
        //}

        /// <summary>
        /// Method associated with all the GetUser Methods. 
        /// It is responsible for receiving a string from GetUser Methods, creating an instance of User with the parameters contained in the string and returning it.
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public User BuildUser(string userString)
        {
            // Split the user string into individual parameters
            string[] parameters = userString.Split('|');

            // Define a default image
            string imageUrl = string.IsNullOrEmpty(parameters[7]) ? "https://i.ibb.co/ZKV9y5r/da7ed7b0-5f66-4f97-a610-51100d3b9fd2.jpg" : parameters[7];

            // Convert integer representations to boolean
            bool isBlocked = parameters[8] == "1";
            bool isAdmin = parameters[9] == "1";

            // Create a new User object and populate its properties
            User user = new User
            {
                Id = int.Parse(parameters[0]),
                Username = parameters[1],
                Password = parameters[2],
                Email = parameters[3],
                FirstName = parameters[4],
                LastName = parameters[5],
                ContentBio = parameters[6],
                ImageSource = imageUrl,
                IsAdmin = isAdmin,
                IsBlocked = isBlocked,
            };

            return user;
        }

        public List<User> GetAllUsers()
        {
            // Retrieve the list of user strings from the repository
            List<string> userString = _userRepository.GetAllUsers();

            List<User> returnUsers = new List<User>();
            User user = new User();

            foreach (string strg in userString)
            {
                // Call User builder method and assign it 
                user = BuildUser(strg);
                returnUsers.Add(user);
            }

            return returnUsers;
        }

        public List<User> GetUserByName(string username)
        {
            if (string.IsNullOrEmpty(username))
                username = "";

            // Retrieve the list of User parameters through a string from the repository
            List<string> userStrings = _userRepository.GetUserByName(username);

            List<User> returnUsers = new List<User>();
            User user = new User();

            foreach (string strg in userStrings)
            {
                // Call User builder method and assign it 
                user = BuildUser(strg);

                // Add the User to return list
                returnUsers.Add(user);
            }

            return returnUsers;
        }

        public User GetUserById(int userId)
        {
            // Retrieve the list of User parameters through a string from the repository
            string userString = _userRepository.GetUserById(userId);

            // Call and return Recipe builder method
            return BuildUser(userString);
        }

        /// <summary>
        /// Get a User by it status. It returns the users that have the corresponding parameter as true.
        /// (Possible parameters: admin, blocked)
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<User> GetUserByStatus(string parameter)
        {
            if (parameter.Trim().ToLower() == "admin" || parameter.Trim().ToLower() == "blocked")
            {
                // Retrieve the list of user strings from the repository
                List<string> userString = _userRepository.GetUserByStatus(parameter);

                List<User> returnUsers = new List<User>();
                User user = new User();

                foreach (string strg in userString)
                {
                    // Call User builder method and assign it 
                    user = BuildUser(strg);

                    // Add the User to return list
                    returnUsers.Add(user);
                }

                return returnUsers;
            }

            return null;
        }
    }
}
