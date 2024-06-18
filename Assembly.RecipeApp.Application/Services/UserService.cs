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
        private readonly IRecipeRepository _recipeRepository;
        private readonly ICommentRepository _commentRepository;

        public UserService(IUserRepository userRepository, IRecipeRepository recipeRepository, ICommentRepository commentRepository) 
        { 
            _userRepository = userRepository;
            _recipeRepository = recipeRepository;
            _commentRepository = commentRepository;
        }

        public List<User> GetAll()
        {
            List <User> usersList = new List<User>();

            foreach (User u in _userRepository.GetAll())
            {
                if (u.Username != "Deleted user")
                {
                    usersList.Add(u);
                }
            }

            return usersList;
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

                if (u.Username == "Deleted User")
                {
                    filteredUsers.Remove(u);
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
            List<User> usersList = new List<User>();

            foreach (User u in _userRepository.GetUsers(currentPage, pageSize))
            {
                if (u.Username != "Deleted User")
                {
                    usersList.Add(u);
                }
            }

            return usersList;
        } // Feito

        public List<User> GetBlockedUsers(int currentPage, int pageSize)
        {
            List<User> usersList = new List<User>();

            foreach (User u in _userRepository.GetBlockedUsers(currentPage, pageSize))
            {
                if (u.Username != "Deleted User")
                {
                    usersList.Add(u);
                }
            }

            return usersList;
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
            bool result = true;
            User updatedUser = new User(user.Id, "Deleted User", "NoPassword1", "deleteduser@mail.com", "Deleted", "User", null, null, false, true, DateTime.Now);

            List<Recipe> userRecipes = _recipeRepository.GetByUserId(user.Id);
            List<Comment> userComments = _commentRepository.GetByUserId(user.Id);

            if ((userRecipes == null || !userRecipes.Any()) && (userComments == null || !userComments.Any()))
                result = _userRepository.Delete(user);

            if(userRecipes != null || userComments != null)
            {
                result = _userRepository.UpdateById(updatedUser);
            }

            return result;
        } // Feito

        public User Login(string username, string password)
        {
            return _userRepository.Login(username, password);
        } // Feito
         
    }
}
