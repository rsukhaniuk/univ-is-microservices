namespace SmartMenu.Services.ProductAPI.Models.Dto
{
    /// <summary>
    /// Represents a data transfer object for a product.
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
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
        /// Gets or sets the category identifier for the product.
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
        /// Gets or sets the image file of the product.
        /// </summary>
        public IFormFile? Image { get; set; }

        /// <summary>
        /// Gets or sets the category data transfer object for the product.
        /// </summary>
        public CategoryDto? Category { get; set; }
    }
}
