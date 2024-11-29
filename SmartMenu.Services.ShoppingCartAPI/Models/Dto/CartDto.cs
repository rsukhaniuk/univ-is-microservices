namespace SmartMenu.Services.ShoppingCartAPI.Models.Dto
{
    /// <summary>
    /// Represents a data transfer object for a shopping cart.
    /// </summary>
    public class CartDto
    {
        /// <summary>
        /// Gets or sets the cart header.
        /// </summary>
        public CartHeaderDto CartHeader { get; set; }

        /// <summary>
        /// Gets or sets the cart details.
        /// </summary>
        public IEnumerable<CartDetailsDto>? CartDetails { get; set; }
    }
}
