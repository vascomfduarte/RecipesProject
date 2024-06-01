using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository.Interfaces;
using Assembly.RecipeApp.Repository.Repos;
using System.Text.RegularExpressions;

namespace Assembly.RecipeApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) 
        { 
            _userRepository = userRepository;
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll();
        } // Feito 

        public List<User> GetAllBlocked()
        {
            List<User> filteredUsers = new List<User>();

            foreach (User u in _userRepository.GetAll())
            {
                if (u.IsBlocked is true)
                {
                    filteredUsers.Add(u);
                }
            }

            return filteredUsers;
        } // Feito 

        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        } // Feito 

        public User GetByUsername(string username)
        {
            return _userRepository.GetByUsername(username);
        } // Feito 

        public List<User> GetFilteredUsers(string name)
        {
            return _userRepository.GetFilteredUsers(name.ToLower());
        } // Feito 

        public List<User> GetUsers(int currentPage, int pageSize)
        {
            return _userRepository.GetUsers(currentPage, pageSize);
        } // Feito

        public List<User> GetBlockedUsers(int currentPage, int pageSize)
        {
            return _userRepository.GetBlockedUsers(currentPage, pageSize);
        } // Feito

        public bool Add(User user)
        {
            // Validate if username already exist
            if (GetAll().Any(u => u.Username == user.Username))
                throw new ArgumentException("Username already exists.", nameof(user.Username));

            // Validate if email already exist
            if (GetAll().Any(u => u.Email == user.Email))
                throw new ArgumentException("Email address already exists.", nameof(user.Email));

            // Set isAdmin to false by default if not provided
            user.SetAdminDefault(user);

            // Set isBlocked to false by default if not provided
            user.SetBlockedDefault(user);
                       
            if(_userRepository.Add(user))
            {
                return true;
            }

            return false;

        } // Feito

        public bool Update(User user)
        {
            return _userRepository.Update(user);

        } // Feito

        public bool UpdateFromString(string newUserDataString, User previousUser)
        {
            try
            {
                // Check if the newUserDataString is null or empty
                if (string.IsNullOrEmpty(newUserDataString))
                {
                    // Return false indicating that the update failed due to invalid data
                    return false;
                }

                // Split the newUserDataString into individual properties, preserving spaces
                string[] userData = newUserDataString.Split(new[] { '|' }, StringSplitOptions.None);

                // Extract individual properties from the userData array
                string username = userData[0];
                string password = userData[1];
                string email = userData[2];
                string firstName = userData[3];
                string lastName = userData[4];
                string contentBio = userData[5];
                string imageSource = userData[6];

                // Update only the properties that have changed and are not null or empty
                if (!string.IsNullOrEmpty(username) && username != previousUser.Username)
                {
                    previousUser.Username = username;
                }

                if (!string.IsNullOrEmpty(password) && password != previousUser.Password)
                {
                    previousUser.Password = password;
                }

                if (!string.IsNullOrEmpty(email) && email != previousUser.Email)
                {
                    previousUser.Email = email;
                }

                if (!string.IsNullOrEmpty(firstName) && firstName != previousUser.FirstName)
                {
                    previousUser.FirstName = firstName;
                }

                if (!string.IsNullOrEmpty(lastName) && lastName != previousUser.LastName)
                {
                    previousUser.LastName = lastName;
                }

                if (!string.IsNullOrEmpty(contentBio) && contentBio != previousUser.ContentBio)
                {
                    previousUser.ContentBio = contentBio;
                }

                if (!string.IsNullOrEmpty(imageSource) && imageSource != previousUser.ImageSource)
                {
                    previousUser.ImageSource = imageSource;
                }

                // Call the repository method to update the user
                return _userRepository.Update(previousUser);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                // Log the exception or return false indicating the update failed
                return false;
            }
        }

        public bool UpdateAdminStatus(User user, User adminUser)
        {
            // Validate if user is admin
            // Send Build user and send you to database 

            throw new NotImplementedException();
        }

        public bool Delete(User user)
        {
            return _userRepository.Delete(user);
        } // Feito

        public User Login(string username, string password)
        {
            return _userRepository.Login(username, password);
        } // Feito
         
    }
}
