using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;

namespace Assembly.RecipeApp.Repository.Repos
{
    public class CategoryRepository : ICategoryRepository
    {
        private static string _connectionString = ConnectionStringProvider.GetConnectionString();

        public Category Category;

        public List<Category> GetAll()
        {
            List<Category> categories = new List<Category>();

            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM category;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            DateTime date = reader.GetDateTime(2);

                            var category = new Category(id, name, date);

                            categories.Add(category);
                        }
                    }
                }
            }

            return categories;
        } // Feito

        public Category GetById(int id)
        {
            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM category WHERE id = @id;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameter to command
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int categoryId = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            DateTime date = reader.GetDateTime(2);

                            Category = new Category(categoryId, name, date);
                        }
                    }
                }
            }

            return Category;
        } // Feito

        public List<Category> GetByRecipeId(int recipeId)
        {
            List<Category> categories = new List<Category>();

            // Collect data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                //string query = "SELECT * FROM category;";

                string query = "SELECT c.[id], c.[name], c.[created_date]" +
                               "FROM [dbo].[recipe_categories] AS rc" +
                               "INNER JOIN [dbo].[category] AS c ON rc.[category_id] = c.[id]" +
                               "WHERE rc.[recipe_id] = @id;";


                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameter to command
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = recipeId;

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            DateTime createdDate = reader.GetDateTime(2);

                            var category = new Category(id, name, createdDate);

                            categories.Add(category);
                        }
                    }
                }
            }

            return categories;
        } // Feito

        public bool Add(Category entity, User adminUser)
        {
            throw new NotImplementedException();
        }
        public bool Update(Category entity, User adminUser)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id, User adminUser)
        {
            throw new NotImplementedException();
        }

    }
}
