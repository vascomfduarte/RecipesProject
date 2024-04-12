using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Application.Services;
using Assembly.RecipeApp.Repository.Interfaces;
using Assembly.RecipeApp.Repository.Repos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Assembly.RecipeApp.Di
{
    public static class ServiceRegistration
    {
        public static void AddConsoleServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.AddCommonServices(configuration);
        }

        public static void AddRazorServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCommonServices(configuration);
        }

        private static void AddCommonServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ICategoryService, CategoryService>();
            services.AddSingleton<ICommentService, CommentService>();
            services.AddSingleton<IDifficultyService, DifficultyService>();
            services.AddSingleton<IIngredientService, IngredientService>();
            services.AddSingleton<IPreparationMethodService, PreparationMethodService>();
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IRatingService, RatingService>();
            services.AddSingleton<IRecipeService, RecipeService>();
            services.AddSingleton<IUnitService, UnitService>();
            services.AddSingleton<IUserService, UserService>();

            services.AddSingleton<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<ICommentRepository, CommentRepository>();
            services.AddSingleton<IDifficultyRepository, DifficultyRepository>();
            services.AddSingleton<IIngredientRepository, IngredientRepository>();
            services.AddSingleton<IPreparationMethodRepository, PreparationMethodRepository>();
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<IRatingRepository, RatingRepository>();
            services.AddSingleton<IRecipeRepository, RecipeRepository>();
            services.AddSingleton<IUnitRepository, UnitRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
        }
    }

}
