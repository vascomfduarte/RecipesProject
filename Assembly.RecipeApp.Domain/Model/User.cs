﻿using Assembly.RecipeApp.Domain.Exceptions;
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

        public User(string username, string password, string email, string firstName, string lastName) 
            : this(username, password, email)
        {
            FirstName = firstName;
            LastName = lastName;
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
            : this(id, username, password, email, firstName, lastName, contentBio, imageSource)
        {
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

        public void SetIsAdmin(User currentUser, bool isAdmin)
        {
            // Check if the current user is an admin
            if (currentUser != null && currentUser.IsAdmin)
            {
                IsAdmin = isAdmin;
            }
            else
            {
                throw new DomainException("Only admin users can change the IsAdmin status.");
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
                throw new DomainException("Only admin users can change the IsBlocked status.");
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