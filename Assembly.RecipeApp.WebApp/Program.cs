using Assembly.RecipeApp.Application.Interfaces;
using Assembly.RecipeApp.Application.Services;
using Assembly.RecipeApp.Domain.Model;
using Assembly.RecipeApp.Repository.Interfaces;
using Assembly.RecipeApp.Repository.Repos;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. (DI Framework)
builder.Services.AddRazorPages();
//builder.Services.AddRazorServices(builder.Configuration);

//builder.Services.AddSingleton<ICategoryService, CategoryService>();
//builder.Services.AddSingleton<ICommentService, CommentService>();
//builder.Services.AddSingleton<INoteService, NoteService>();
//builder.Services.AddSingleton<IDifficultyService, DifficultyService>();
//builder.Services.AddSingleton<IIngredientService, IngredientService>();
//builder.Services.AddSingleton<IPreparationMethodService, PreparationMethodService>();
//builder.Services.AddSingleton<IProductService, ProductService>();
//builder.Services.AddSingleton<IRatingService, RatingService>();
//builder.Services.AddSingleton<IRecipeService, RecipeService>();
//builder.Services.AddSingleton<IUnitService, UnitService>();
//builder.Services.AddSingleton<IUserService, UserService>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<IDifficultyService, DifficultyService>();
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<IPreparationMethodService, PreparationMethodService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<IUserService, UserService>();

//builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();
//builder.Services.AddSingleton<ICommentRepository, CommentRepository>();
//builder.Services.AddSingleton<INoteRepository, NoteRepository>();
//builder.Services.AddSingleton<IDifficultyRepository, DifficultyRepository>();
//builder.Services.AddSingleton<IIngredientRepository, IngredientRepository>();
//builder.Services.AddSingleton<IPreparationMethodRepository, PreparationMethodRepository>();
//builder.Services.AddSingleton<IProductRepository, ProductRepository>();
//builder.Services.AddSingleton<IRatingRepository, RatingRepository>();
//builder.Services.AddSingleton<IRecipeRepository, RecipeRepository>();
//builder.Services.AddSingleton<IUnitRepository, UnitRepository>();
//builder.Services.AddSingleton<IUserRepository, UserRepository>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<IDifficultyRepository, DifficultyRepository>();
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddScoped<IPreparationMethodRepository, PreparationMethodRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IUnitRepository, UnitRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddSession( option =>
{ 
    option.IdleTimeout = TimeSpan.FromMinutes(5);
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;
});

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

app.UseSession();

app.MapRazorPages();

app.Run();
