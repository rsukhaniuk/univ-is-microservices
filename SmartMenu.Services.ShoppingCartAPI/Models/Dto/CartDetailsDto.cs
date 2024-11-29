
namespace SmartMenu.Services.ShoppingCartAPI.Models.Dto
{
    /// <summary>
    /// Represents a data transfer object for cart details.
    /// </summary>
    public class CartDetailsDto
    {
        /// <summary>
        /// Gets or sets the cart details identifier.
        /// </summary>
        public int CartDetailsId { get; set; }

        /// <summary>
        /// Gets or sets the cart header identifier.
        /// </summary>
        public int CartHeaderId { get; set; }

        /// <summary>
        /// Gets or sets the cart header.
        /// </summary>
        public CartHeaderDto? CartHeader { get; set; }

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
    }
}
