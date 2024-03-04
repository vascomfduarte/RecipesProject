﻿using Assembly.RecipeApp.Application.Interface;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository;
using System.Text.RegularExpressions;

namespace Assembly.RecipeApp.Application.Services
{
    public class UserServices : IUserService
    {

        private static UserRepository _userRepository = new UserRepository();

        //public (bool success, string errorMessage) CreateUser(string username,
        //                       string password,
        //                       string email,
        //                       string firstName,
        //                       string lastName,
        //                       string contentBio = "Let others know who you are",
        //                       string imageSource = "https://i.ibb.co/ZKV9y5r/da7ed7b0-5f66-4f97-a610-51100d3b9fd2.jpg",
        //                       string isAdmin = "0",
        //                       string isBlocked = "0")
        //{
        //    // Validate username
        //    if (string.IsNullOrWhiteSpace(username) || GetAll().Any(u => u.Username == username))
        //        return (false, "Invalid username or username already exists.");

        //    // Validate password strength (Example: Minimum 8 characters with at least one letter and one digit)
        //    if (string.IsNullOrWhiteSpace(password) || password.Length < 8 || !password.Any(char.IsLetter) || !password.Any(char.IsDigit))
        //        return (false, "Invalid password. Password must be at least 8 characters long and contain at least one letter and one digit.");

        //    // Validate email format
        //    if (string.IsNullOrWhiteSpace(email) ||
        //        !Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$") ||
        //        GetAll().Any(u => u.Email == email))
        //        return (false, "Invalid email address or email address already exists.");

        //    // Validate first name and last name format
        //    if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
        //        return (false, "First name and last name are required.");

        //    // Validate image source format
        //    if (!Uri.TryCreate(imageSource, UriKind.Absolute, out Uri uriResult) || uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps)
        //        return (false, "Invalid image URL.");

        //    // Set isAdmin to 0 by default if not provided
        //    if (isAdmin != "1" && isAdmin != "0")
        //        isAdmin = "0";

        //    // Call repository method to create user
        //    bool success = _userRepository.CreateUser(username, password, email, firstName, lastName, contentBio, imageSource, isAdmin, isBlocked);
        //    return (success, success ? "" : "Failed to create user. Please try again later.");
        //}

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


        public bool Add(User user)
        {
            // Validate if username already exist
            if (GetAll().Any(u => u.Username == user.Username))
                throw new ArgumentException("Username already exists.", nameof(user.Username));

            // Validate if email already exist
            if (GetAll().Any(u => u.Email == user.Email))
                throw new ArgumentException("Email address already exists.", nameof(user.Email));

            // Validate image source format
            if (user.ImageSource is not null)
            {
                if (!Uri.TryCreate(user.ImageSource, UriKind.Absolute, out Uri uriResult) || uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps)
                {
                    user.ImageSource = "https://i.ibb.co/ZKV9y5r/da7ed7b0-5f66-4f97-a610-51100d3b9fd2.jpg";
                    //throw new ArgumentException("Invalid image URL.", nameof(user.ImageSource));
                }
            }
            else if (user.ImageSource is null)
            {
                // Set a default to ImageSource
                user.ImageSource = "https://i.ibb.co/ZKV9y5r/da7ed7b0-5f66-4f97-a610-51100d3b9fd2.jpg";
            }

            // Set a default to contentBio
            if (user.ContentBio is null)
                user.ContentBio = "Let others know who you are";

            // Set isAdmin to false by default if not provided
            if (user.IsAdmin == null || user.IsAdmin == true)
                user.SetIsAdmin(user, false);

            // Set isBlocked to false by default if not provided
            if (user.IsBlocked == null || user.IsBlocked == true)
                user.SetIsBlocked(user, false);

            // Format the User object's properties into a string representation
            string userString = $"{user.Username}|{user.Password}|{user.Email}|{user.FirstName}|{user.LastName}|{user.ContentBio}|{user.ImageSource}|{(user.IsAdmin ? "1" : "0")}|{(user.IsBlocked ? "1" : "0")}";

            // Call the UserRepository's Add method with the formatted string representation of the User
            return _userRepository.Add(userString);
        }

        public List<User> GetAll()
        {
            // Retrieve the list of user strings from the repository
            List<string> userStrings = _userRepository.GetAll();

            List<User> returnUsers = new List<User>();

            foreach (string userString in userStrings)
            {
                // Parse user string to extract user data
                User user = ParseUser(userString);

                // Add the parsed user to the list
                returnUsers.Add(user);
            }

            return returnUsers;
        }

        public User GetById(int id)
        {
            // Retrieve the list of User parameters through a string from the repository
            string userString = _userRepository.GetById(id);

            // Call and return Recipe builder method
            return ParseUser(userString);
        }

        public List<User> GetFilteredUsers(string name)
        {
            if (string.IsNullOrEmpty(name))
                name = "";

            // Retrieve the list of User parameters through a string from the repository
            List<string> userStrings = _userRepository.GetFilteredUsers(name.ToLower());

            List<User> returnUsers = new List<User>();

            foreach (string strg in userStrings)
            {
                // Call User builder method and assign it 
                User user = ParseUser(strg);

                // Add the User to return list
                returnUsers.Add(user);
            }

            return returnUsers;
        }

        public bool Update(User user)
        {
            // Passei validação para repositorio

            //if (user.Id == 0)
            //{
            //    List <User> oldUser = GetFilteredUsers(user.Username);

            //    foreach (User u in oldUser)
            //    {
            //        user.Id = u.Id; // Não é possível pois ID é privado
            //    }
            //}

            // Validate image source format
            if (!String.IsNullOrEmpty(user.ImageSource))
            {
                if (!Uri.TryCreate(user.ImageSource, UriKind.Absolute, out Uri uriResult) || uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps)
                {
                    user.ImageSource = "https://i.ibb.co/ZKV9y5r/da7ed7b0-5f66-4f97-a610-51100d3b9fd2.jpg";
                    //throw new ArgumentException("Invalid image URL.", nameof(user.ImageSource));
                }
            }
            else if (String.IsNullOrEmpty(user.ImageSource))
            {
                // Set a default to ImageSource
                user.ImageSource = "https://i.ibb.co/ZKV9y5r/da7ed7b0-5f66-4f97-a610-51100d3b9fd2.jpg";
            }

            // Set a default to contentBio
            if (String.IsNullOrEmpty(user.ContentBio))
                user.ContentBio = "Let others know who you are";

            // Format the User object's properties into a string representation
            string userString = $"{user.Id}|{user.Username}|{user.Password}|{user.Email}|{user.FirstName}|{user.LastName}|{user.ContentBio}|{user.ImageSource}";

            return _userRepository.Update(userString);
        }
        public bool UpdateIsAdmin(User adminUser, User userToModify)
        {
            throw new NotImplementedException();
        }
        public bool UpdateIsBlocked(User adminUser, User userToModify)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method associated with all the get methods from User. 
        /// It is responsible for receiving a string, creating an instance of User with the parameters contained in the string and returning it.
        /// </summary>
        /// <param userString="parameter"></param>
        /// <returns></returns>
        public User ParseUser(string userString)
        {
            // Split the user string into individual parameters
            string[] userData = userString.Split('|');

            // Extract individual data
            int id = int.Parse(userData[0]);
            string username = userData[1];
            string password = userData[2];
            string email = userData[3];
            string firstName = userData[4];
            string lastName = userData[5];
            string contentBio = userData[6];            
            string imageSource = string.IsNullOrEmpty(userData[7]) ? "https://i.ibb.co/ZKV9y5r/da7ed7b0-5f66-4f97-a610-51100d3b9fd2.jpg" : userData[7]; // Define a default image
            // Convert integer representations to boolean
            bool isBlocked = userData[8] == "1";
            bool isAdmin = userData[9] == "1";

            // Create a new User object with the extracted data
            User user = new User(id,
                                 username,
                                 password,
                                 email,
                                 firstName,
                                 lastName,
                                 contentBio,
                                 imageSource,
                                 isBlocked,
                                 isAdmin);

            // Additional processing for Comments and Recipes if needed

            return user;
        }

    }
}
