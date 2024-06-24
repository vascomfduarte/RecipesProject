namespace Assembly.RecipeApp.Repository.Repos
{
    public class ConnectionStringProvider
    {
        private static readonly string _connectionString;

        static ConnectionStringProvider()
        {

            // Initialize _connectionString from configuration or other source

             _connectionString = "Server=DESKTOP-VUIIR3S;Database=JD_FC_VD_RecipesProject;Integrated Security=True;"; // Torre

            // _connectionString = "Server=;Database=JD_FC_VD_RecipesProject;Integrated Security=True;"; // Portatil

        }

        public static string GetConnectionString()
        {
            return _connectionString;
        }

    }
}
