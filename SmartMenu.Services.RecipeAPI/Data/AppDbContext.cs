using Microsoft.EntityFrameworkCore;
using SmartMenu.Services.RecipeAPI.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using static SmartMenu.Services.RecipeAPI.Utility.SD;

namespace SmartMenu.Services.RecipeAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<IngridientRecipe> ProductRecipe { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeding Categories (Dish Categories)
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    Name = "Pasta"
                },
                new Category
                {
                    CategoryId = 2,
                    Name = "Pizza"
                },
                new Category
                {
                    CategoryId = 3,
                    Name = "Dessert"
                }
            );

            // Seeding Ingredients
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient
                {
                    IngredientId = 1,
                    Name = "Tomato"
                },
                new Ingredient
                {
                    IngredientId = 2,
                    Name = "Basil"
                },
                new Ingredient
                {
                    IngredientId = 3,
                    Name = "Mozzarella"
                },
                new Ingredient
                {
                    IngredientId = 4,
                    Name = "Olive Oil"
                },
                new Ingredient
                {
                    IngredientId = 5,
                    Name = "Flour"
                },
                new Ingredient
                {
                    IngredientId = 6,
                    Name = "Egg"
                },
                new Ingredient
                {
                    IngredientId = 7,
                    Name = "Sugar"
                },
                new Ingredient
                {
                    IngredientId = 8,
                    Name = "Butter"
                }
            );

            // Seeding Recipes
            modelBuilder.Entity<Recipe>().HasData(
                new Recipe
                {
                    RecipeId = 1,
                    Name = "Margherita Pizza",
                    Price = 12.50,
                    Description = "Classic pizza with tomato, mozzarella, and fresh basil.",
                    CategoryId = 2,
                    ImageUrl = "https://placehold.co/600x400"
                },
                new Recipe
                {
                    RecipeId = 2,
                    Name = "Tiramisu",
                    Price = 8.99,
                    Description = "Traditional Italian dessert made with coffee-soaked ladyfingers and mascarpone.",
                    CategoryId = 3,
                    ImageUrl = "https://placehold.co/600x400"
                },
                new Recipe
                {
                    RecipeId = 3,
                    Name = "Spaghetti Carbonara",
                    Price = 10.99,
                    Description = "Creamy pasta with pancetta, egg, and cheese.",
                    CategoryId = 1,
                    ImageUrl = "https://placehold.co/600x400"
                }
            );

            // Seeding IngridientRecipe Relationships
            modelBuilder.Entity<IngridientRecipe>().HasData(
                new IngridientRecipe
                {
                    IngridientRecipeId = 1,
                    RecipeId = 1, // Margherita Pizza
                    IngredientId = 1, // Tomato
                    Quantity = 3,
                    Unit = MeasurementUnit.Piece
                },
                new IngridientRecipe
                {
                    IngridientRecipeId = 2,
                    RecipeId = 1, // Margherita Pizza
                    IngredientId = 2, // Basil
                    Quantity = 5,
                    Unit = MeasurementUnit.Piece
                },
                new IngridientRecipe
                {
                    IngridientRecipeId = 3,
                    RecipeId = 1, // Margherita Pizza
                    IngredientId = 3, // Mozzarella
                    Quantity = 250,
                    Unit = MeasurementUnit.Kilogram
                },
                new IngridientRecipe
                {
                    IngridientRecipeId = 4,
                    RecipeId = 2, // Tiramisu
                    IngredientId = 6, // Egg
                    Quantity = 3,
                    Unit = MeasurementUnit.Piece
                },
                new IngridientRecipe
                {
                    IngridientRecipeId = 5,
                    RecipeId = 2, // Tiramisu
                    IngredientId = 7, // Sugar
                    Quantity = 100,
                    Unit = MeasurementUnit.Kilogram
                },
                new IngridientRecipe
                {
                    IngridientRecipeId = 6,
                    RecipeId = 2, // Tiramisu
                    IngredientId = 8, // Butter
                    Quantity = 200,
                    Unit = MeasurementUnit.Kilogram
                },
                new IngridientRecipe
                {
                    IngridientRecipeId = 7,
                    RecipeId = 3, // Spaghetti Carbonara
                    IngredientId = 6, // Egg
                    Quantity = 2,
                    Unit = MeasurementUnit.Piece
                },
                new IngridientRecipe
                {
                    IngridientRecipeId = 8,
                    RecipeId = 3, // Spaghetti Carbonara
                    IngredientId = 4, // Olive Oil
                    Quantity = 50,
                    Unit = MeasurementUnit.Liter
                }
            );
        }
    }
}
