using static System.Net.WebRequestMethods;

namespace Assembly.RecipeApp.Domain.Model
{
    public class User
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
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
        }

        public User(int id, string username, string password, string email)
        { }

        // Construtores para definir estado inicial do objeto
        // A class é que se conhece a si mesma. Validações de parâmetros feitas aqui. 
    }
}
