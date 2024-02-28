using Model;
using Repository;
using static System.Net.WebRequestMethods;

namespace Services
{
    public class UserServices
    {
        private static UserRepository _userRepository = new UserRepository();

        public bool CreateUser(string username,
                               string password,
                               string email,
                               string firstName,
                               string lastName,
                               string contentBio = "Let others know who you are",
                               string imageSource = "https://i.ibb.co/ZKV9y5r/da7ed7b0-5f66-4f97-a610-51100d3b9fd2.jpg",
                               string isAdmin = "0",
                               string isBlocked = "0")
        {
            bool success = _userRepository.CreateUser(username,
                                                      password,
                                                      email,
                                                      firstName,
                                                      lastName,
                                                      contentBio,
                                                      imageSource,
                                                      isAdmin,
                                                      isBlocked);

            return true; 
        }

        public User BuildUser (string userString)
        {
            // Split the user string into individual parameters
            string[] parameters = userString.Split('|');

            // Define a default image
            string imageUrl = String.IsNullOrEmpty(parameters[7]) ? "https://i.ibb.co/ZKV9y5r/da7ed7b0-5f66-4f97-a610-51100d3b9fd2.jpg" : parameters[7];

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

                // Check if the constructed Recipe object is approved and add it to the returnlist
                if (true) //recipe.IsApproved           
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
                    returnUsers.Add(user);
                }

                return returnUsers;
            }

            return null;
        }
    }
}