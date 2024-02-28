using static System.Net.WebRequestMethods;

namespace Assembly.Recipe.Domain.Model
{
    public class User
    {
        public int Id { get; private set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContentBio { get; set; }
        public string ImageSource { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBlocked { get; set; }

        public List<Comment> Comments { get; set; }
        public List<Recipe> Recipes { get; set; }
        //public List<UserFavoriteRecipe> UserFavoriteRecipes { get; set; }

        public User(string username, string password, string email)
        { }

        public User(int id, string username, string password, string email)
        { }

        // Construtores para definir estado inicial do objeto
        // A class é que se conhece a si mesma. Validações de parâmetros feitas aqui. 
    }
}
