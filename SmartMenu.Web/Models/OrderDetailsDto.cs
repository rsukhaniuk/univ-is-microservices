
namespace SmartMenu.Web.Models
{
    /// <summary>
    /// Data transfer object for order details.
    /// </summary>
    public class OrderDetailsDto
    {
        /// <summary>
        /// Gets or sets the order details identifier.
        /// </summary>
        public int OrderDetailsId { get; set; }

        /// <summary>
        /// Gets or sets the order header identifier.
        /// </summary>
        public int OrderHeaderId { get; set; }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product details.
        /// </summary>
        public ProductDto? Product { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public double Price { get; set; }
    }
}
