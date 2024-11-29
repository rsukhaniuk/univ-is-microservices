

namespace SmartMenu.Services.OrderAPI.Models.Dto
{
    /// <summary>
    /// Represents a product.
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// Gets or sets the product ID.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the category ID.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category of the product.
        /// </summary>
        public CategoryDto? Category { get; set; }

        /// <summary>
        /// Gets or sets the image URL of the product.
        /// </summary>
        public string ImageUrl { get; set; }
    }
}
