using System.ComponentModel.DataAnnotations;

namespace SmartMenu.Services.ShoppingCartAPI.Models.Dto
{
    /// <summary>
    /// Represents a data transfer object for a product.
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// Gets or sets the product identifier.
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
        /// Gets or sets the category identifier.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category details.
        /// </summary>
        public CategoryDto? Category { get; set; }

        /// <summary>
        /// Gets or sets the URL of the product image.
        /// </summary>
        public string ImageUrl { get; set; }
    }
}
