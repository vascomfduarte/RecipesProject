using Model;
using Repository;

namespace Services
{
    public class UserServices
    {
        private static UserRepository _userRepository = new UserRepository();

        public List<User> GetUserById(int userId)
        {
            // Retrieve the list of recipe strings from the repository
            List<string> userString = _userRepository.GetUserById(userId);

            List<User> returnUsers = new List<User>();

            // Process each recipe string
            foreach (string userParameters in userString)
            {
                // Split the recipe string into individual parameters
                string[] parameters = userParameters.Split('|');

                // Convert integer representations to boolean
                bool isBlocked = parameters[8] == "1";
                bool isAdmin = parameters[9] == "1";

                // Define a default image
                string imageUrl = String.IsNullOrEmpty(parameters[7]) ? "https://i.ibb.co/ZKV9y5r/da7ed7b0-5f66-4f97-a610-51100d3b9fd2.jpg" : parameters[7];

                // Create a new Recipe object and populate its properties
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

                // Add the constructed User object to the returnlist
                returnUsers.Add(user);
            }

            return returnUsers;
        }
    }
}