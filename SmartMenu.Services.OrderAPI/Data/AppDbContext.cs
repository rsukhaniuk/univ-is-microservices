using SmartMenu.Services.OrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SmartMenu.Services.OrderAPI.Data
{
    /// <summary>
    /// Represents the database context for the Order API.
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
        /// Gets or sets the DbSet for order headers.
        /// </summary>
        public DbSet<OrderHeader> OrderHeaders { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for order details.
        /// </summary>
        public DbSet<OrderDetails> OrderDetails { get; set; }
    }
}
