using Assembly.RecipeApp.Domain.Exceptions;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

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
        
        public string _firstName { get; private set; }
        public string FirstName
        {
            get { return _firstName; }
            set 
            {
                ValidateFirstName(value);
                _firstName = value;
            }
        }

        public string _lastName { get; private set; }
        public string LastName
        {
            get { return _lastName; }
            set 
            {
                ValidateLastName(value);
                _lastName = value;
            }
        }

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
        }

        public User(string username, string password, string email, string firstName, string lastName)
            : this(username, password, email)
        {
            FirstName = firstName;
            LastName = lastName;
            IsAdmin = false;
            IsBlocked = false;
        }

        public User(string username, string password, string email, string firstName, string lastName, string contentBio, string imageSource)
            : this(username, password, email, firstName, lastName)
        {
            ContentBio = contentBio;
            ImageSource = imageSource;
        }

        public User(int id, string username, string password, string email, string firstName, string lastName, string contentBio, string imageSource)
            : this(username, password, email, firstName, lastName, contentBio, imageSource)
        {
            Id = id;
        }

        public User(int id, string username, string password, string email, string firstName, string lastName, string contentBio, string imageSource, bool isAdmin, bool isBlocked)
            : this(username, password, email)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            ContentBio = contentBio;
            ImageSource = imageSource;
            IsAdmin = isAdmin;
            IsBlocked = isBlocked;
        }
        
        // Construtores para definir estado inicial do objeto
        // A class é que se conhece a si mesma. Validações de parâmetros feitas aqui. 

        private void ValidateUsername(string username)
        {
            // Check if username is null or empty
            if (string.IsNullOrEmpty(username))
            {
                throw new DomainException("Username cannot be null or empty.");
            }
        }
        private void ValidatePassword(string password)
        {
            // Check if password is null or empty
            if (string.IsNullOrEmpty(password) || password.Length < 8 || !password.Any(char.IsLetter) || !password.Any(char.IsDigit))
            {
                throw new DomainException("Invalid password. Password must be at least 8 characters long and contain at least one letter and one digit.");
            }
        }
        private void ValidateEmail(string email)
        {
            // Check if email is null or empty
            if (string.IsNullOrEmpty(email))
            {
                throw new DomainException("Email cannot be null or empty.");
            }

            // Check if email format is valid
            if (!Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {                
                throw new DomainException("Invalid email format.");
            }
        }
        private void ValidateFirstName(string firstName)
        {
            // Check if First Name is null or empty
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new DomainException("First name cannot be null or empty.");
            }
            
            if (firstName.Length > 50)
            {
                throw new DomainException("First name cannot exceed 50 characters.");
            }

            // Check if name format is valid
            if (!Regex.IsMatch(firstName, @"^[a-zA-Z]+$"))
            {
                throw new DomainException("First name can only contain letters.");
            }
        }
        private void ValidateLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new DomainException("Last name cannot be null or empty.");
            }
            
            if (lastName.Length > 50)
            {
                throw new DomainException("Last name cannot exceed 50 characters.");
            }

            // Check if name format is valid
            if (!Regex.IsMatch(lastName, @"^[a-zA-Z]+$"))
            {
                throw new DomainException("Last name can only contain letters.");
            }
        }

        public void SetIsAdmin(User currentUser, bool isAdmin)
        {
            // Check if the current user is an admin
            if (currentUser != null)
            {
                IsAdmin = isAdmin;
            }
            else
            {
                throw new DomainException("Unable to change IsAdmin status.");
            }
        }
        public void SetIsBlocked(User currentUser, bool isBlocked)
        {
            // Check if the current user is an admin
            if (currentUser != null)
            {
                IsBlocked = isBlocked;
            }
            else
            {
                throw new DomainException("Unable to change IsBlocked status.");
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
