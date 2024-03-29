using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Application.Services;
//using Assembly.RecipeApp.Di;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository.Interfaces;
using Assembly.RecipeApp.Repository.Repos;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. (DI Framework)
builder.Services.AddRazorPages();
//builder.Services.AddRazorServices(builder.Configuration);

builder.Services.AddSingleton<ICategoryService, CategoryService>();
builder.Services.AddSingleton<ICommentService, CommentService>();
builder.Services.AddSingleton<IDifficultyService, DifficultyService>();
builder.Services.AddSingleton<IIngredientService, IngredientService>();
builder.Services.AddSingleton<IPreparationMethodService, PreparationMethodService>();
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<IRatingService, RatingService>();
builder.Services.AddSingleton<IRecipeService, RecipeService>();
builder.Services.AddSingleton<IUnitService, UnitService>();
builder.Services.AddSingleton<IUserService, UserService>();

builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();
builder.Services.AddSingleton<ICommentRepository, CommentRepository>();
builder.Services.AddSingleton<IDifficultyRepository, DifficultyRepository>();
builder.Services.AddSingleton<IIngredientRepository, IngredientRepository>();
builder.Services.AddSingleton<IPreparationMethodRepository, PreparationMethodRepository>();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<IRatingRepository, RatingRepository>();
builder.Services.AddSingleton<IRecipeRepository, RecipeRepository>();
builder.Services.AddSingleton<IUnitRepository, UnitRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
