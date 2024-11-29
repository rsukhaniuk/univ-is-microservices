using SmartMenu.Web.Utility;
using System.ComponentModel.DataAnnotations;

namespace SmartMenu.Web.Models
{
    /// <summary>
    /// Data transfer object for product details.
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
        /// Gets or sets the URL of the product image.
        /// </summary>
        public string? ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the local path of the product image.
        /// </summary>
        public string? ImageLocalPath { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product.
        /// </summary>
        [Range(1, 100)]
        public int Count { get; set; } = 1;

        /// <summary>
        /// Gets or sets the image file.
        /// </summary>
        [MaxFileSize(1)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile? Image { get; set; }

        /// <summary>
        /// Gets or sets the category details.
        /// </summary>
        public CategoryDto? Category { get; set; }
    }
}
