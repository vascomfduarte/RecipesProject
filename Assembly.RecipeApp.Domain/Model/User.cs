using Assembly.RecipeApp.Domain.Exceptions;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace Assembly.RecipeApp.Domain.Model
{
    public class User
    {
        public int Id { get; private set; }
        public string _username { get; private set; }
        public string Username
        {
            get { return _username; }
            set
            {
                ValidateUsername(value);
                _username = value;
            }
        }
        public string _password { get; private set; }
        public string Password
        {
            get { return _password; }
            set
            {
                ValidatePassword(value);
                _password = value;
            }
        }
        public string _email { get; private set; }
        public string Email
        {
            get { return _email; }
            set
            {
                ValidateEmail(value);
                _email = value;
            }
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContentBio { get; set; }
        public string ImageSource { get; set; }
        public bool IsAdmin { get; private set; }
        public bool IsBlocked { get; private set; }

        public List<Comment> Comments { get; set; }
        public List<Recipe> Recipes { get; set; }
        public List<Recipe> UserFavoriteRecipes { get; set; }

        public User(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
            IsAdmin = false; // Default to non-admin
            IsBlocked = false; // Default to not blocked
        }

        public User(int id, string username, string password, string email) : this(username, password, email)
        {
            Id = id;
        }

        public User(int id, string username, string password, string email, string firstName, string lastname, string contentBio, string imageSource, bool isBlocked, bool isAdmin) 
            : this(id, username, password, email)
        {
            FirstName = firstName;
            LastName = lastname;
            ContentBio = contentBio;
            ImageSource = imageSource;
        }

        // Construtores para definir estado inicial do objeto
        // A class é que se conhece a si mesma. Validações de parâmetros feitas aqui. 

        private void ValidateUsername(string username)
        {
            throw new NotImplementedException();
        }
        private void ValidatePassword(string password)
        {
            throw new NotImplementedException();
        }
        private void ValidateEmail(string password)
        {
            throw new NotImplementedException();
        }


        public void SetIsAdmin(User currentUser, bool isAdmin)
        {
            // Check if the current user is an admin
            if (currentUser != null && currentUser.IsAdmin)
            {
                IsAdmin = isAdmin;
            }
            else
            {
                throw new UnauthorizedAccessException("Only admin users can change the IsAdmin status.");
            }
        }
        public void SetIsBlocked(User currentUser, bool isBlocked)
        {
            // Check if the current user is an admin
            if (currentUser != null && currentUser.IsAdmin)
            {
                IsBlocked = isBlocked;
            }
            else
            {
                throw new UnauthorizedAccessException("Only admin users can change the IsBlocked status.");
            }
        }

        /// <summary>
        /// Method to allow an admin user (currentUser) to change the IsAdmin status of another user (userToModify).
        /// If the user trying to change the status (currentUser) is and Admin, it changes IsAdmin status of userToModify.
        /// </summary>
        /// <param name="currentUser"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void ChangeIsAdminStatus(User currentUser, User userToModify)
        {
            if (currentUser == null || !currentUser.IsAdmin)
            {
                throw new DomainException("Only admin users can give or take admin access from other users.");
            }
            else if (userToModify != null && currentUser.IsAdmin)
            {
                userToModify.IsAdmin = !userToModify.IsAdmin;
            }
            else
            {
                throw new ArgumentNullException(nameof(userToModify), "User cannot be null.");
            }
        }

        /// <summary>
        /// Method to allow an admin user (currentUser) to change the IsBlocked status of another user (userToModify).
        /// If the user trying to change the status (currentUser) is and Admin, it changes IsBlocked status of userToModify.
        /// </summary>
        /// <param name="currentUser"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void ChangeIsBlockedStatus(User currentUser, User userToModify)
        {
            if (currentUser == null || !currentUser.IsAdmin)
            {
                throw new DomainException("Only admin users can Block or Unblock other users.");
            }
            else if (userToModify != null && currentUser.IsAdmin)
            {
                userToModify.IsBlocked = !userToModify.IsBlocked;
            }
            else
            {
                throw new ArgumentNullException(nameof(userToModify), "User cannot be null.");
            }

        }
    }
}
