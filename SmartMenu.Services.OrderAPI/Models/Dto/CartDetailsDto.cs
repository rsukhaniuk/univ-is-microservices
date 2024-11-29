
namespace SmartMenu.Services.OrderAPI.Models.Dto
{
    /// <summary>
    /// Represents the details of a cart.
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
        /// Gets or sets the count of products.
        /// </summary>
        public int Count { get; set; }
    }
}
