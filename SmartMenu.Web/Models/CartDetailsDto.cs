
namespace SmartMenu.Web.Models
{
    /// <summary>
    /// Represents the details of a cart item.
    /// </summary>
    public class CartDetailsDto
    {
        /// <summary>
        /// Gets or sets the cart details ID.
        /// </summary>
        public int CartDetailsId { get; set; }

        /// <summary>
        /// Gets or sets the cart header ID.
        /// </summary>
        public int CartHeaderId { get; set; }

        /// <summary>
        /// Gets or sets the cart header.
        /// </summary>
        public CartHeaderDto? CartHeader { get; set; }

        /// <summary>
        /// Gets or sets the product ID.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        public ProductDto? Product { get; set; }

        /// <summary>
        /// Gets or sets the count of the product.
        /// </summary>
        public int Count { get; set; }
    }
}
