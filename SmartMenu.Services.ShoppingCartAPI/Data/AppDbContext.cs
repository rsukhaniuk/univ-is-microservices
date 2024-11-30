using SmartMenu.Services.ShoppingCartAPI.Models;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace SmartMenu.Services.ShoppingCartAPI.Data
{
    /// <summary>
    /// Represents the database context for the shopping cart API.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class with the specified options.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the DbSet for cart headers.
        /// </summary>
        public DbSet<CartHeader> CartHeaders { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for cart details.
        /// </summary>
        public DbSet<CartDetails> CartDetails { get; set; }
    }
}
