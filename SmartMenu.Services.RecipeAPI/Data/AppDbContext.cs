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
        public DbSet<Recipe> Recipes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Recipe>().HasData(new Recipe
            {
                RecipeId = 1,
                Name = "Bruschetta",
                Price = 7.50,
                Description = "Toasted bread topped with fresh tomatoes, basil, garlic, and a drizzle of olive oil.<br/> A classic Italian appetizer.",
                ImageUrl = "https://placehold.co/603x403",
                CategoryName = "Appetizer"
            });
            modelBuilder.Entity<Recipe>().HasData(new Recipe
            {
                RecipeId = 2,
                Name = "Caprese Salad",
                Price = 9.99,
                Description = "Fresh mozzarella, tomatoes, and basil, drizzled with balsamic glaze.<br/> A light and refreshing start to any meal.",
                ImageUrl = "https://placehold.co/602x402",
                CategoryName = "Appetizer"
            });
            modelBuilder.Entity<Recipe>().HasData(new Recipe
            {
                RecipeId = 3,
                Name = "Tiramisu",
                Price = 10.99,
                Description = "A classic Italian dessert with layers of coffee-soaked ladyfingers and mascarpone cheese, dusted with cocoa powder.",
                ImageUrl = "https://placehold.co/601x401",
                CategoryName = "Dessert"
            });
            modelBuilder.Entity<Recipe>().HasData(new Recipe
            {
                RecipeId = 4,
                Name = "Lasagna",
                Price = 14.50,
                Description = "Layered pasta with rich Bolognese sauce, creamy béchamel, and melted cheese.<br/> A hearty and satisfying Italian main course.",
                ImageUrl = "https://placehold.co/600x400",
                CategoryName = "Entree"
            });
        }
    }
}
