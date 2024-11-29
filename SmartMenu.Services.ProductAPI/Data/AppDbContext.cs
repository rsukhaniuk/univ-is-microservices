using SmartMenu.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SmartMenu.Services.ProductAPI.Data
{
    /// <summary>
    /// Represents the application's database context.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the products in the database.
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the categories in the database.
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Configures the model that was discovered by convention from the entity types
        /// exposed in <see cref="DbSet{TEntity}"/> properties on the derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Appetizer" },
                new Category { CategoryId = 2, Name = "Dessert" },
                new Category { CategoryId = 3, Name = "Entree" }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Name = "Bruschetta",
                    Price = 7.50,
                    Description = "Toasted bread topped with fresh tomatoes, basil, garlic, and a drizzle of olive oil.<br/> A classic Italian appetizer.",
                    ImageUrl = "https://placehold.co/603x403",
                    CategoryId = 1 // Appetizer
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Caprese Salad",
                    Price = 9.99,
                    Description = "Fresh mozzarella, tomatoes, and basil, drizzled with balsamic glaze.<br/> A light and refreshing start to any meal.",
                    ImageUrl = "https://placehold.co/602x402",
                    CategoryId = 1 // Appetizer
                },
                new Product
                {
                    ProductId = 3,
                    Name = "Tiramisu",
                    Price = 10.99,
                    Description = "A classic Italian dessert with layers of coffee-soaked ladyfingers and mascarpone cheese, dusted with cocoa powder.",
                    ImageUrl = "https://placehold.co/601x401",
                    CategoryId = 2 // Dessert
                },
                new Product
                {
                    ProductId = 4,
                    Name = "Lasagna",
                    Price = 14.50,
                    Description = "Layered pasta with rich Bolognese sauce, creamy béchamel, and melted cheese.<br/> A hearty and satisfying Italian main course.",
                    ImageUrl = "https://placehold.co/600x400",
                    CategoryId = 3 // Entree
                }
            );
        }
    }
}
