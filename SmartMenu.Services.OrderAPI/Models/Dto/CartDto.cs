namespace SmartMenu.Services.OrderAPI.Models.Dto
{
    /// <summary>
    /// Represents a shopping cart.
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
