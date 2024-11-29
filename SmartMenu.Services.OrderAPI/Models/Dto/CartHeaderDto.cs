
using System.ComponentModel.DataAnnotations;

namespace SmartMenu.Services.OrderAPI.Models.Dto
{
    /// <summary>
    /// Represents the header of a shopping cart.
    /// </summary>
    public class CartHeaderDto
    {
        /// <summary>
        /// Gets or sets the cart header ID.
        /// </summary>
        public int CartHeaderId { get; set; }

        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// Gets or sets the coupon code.
        /// </summary>
        public string? CouponCode { get; set; }

        /// <summary>
        /// Gets or sets the discount amount.
        /// </summary>
        public double Discount { get; set; }

        /// <summary>
        /// Gets or sets the total amount of the cart.
        /// </summary>
        public double CartTotal { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the user.
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string? Email { get; set; }
    }
}
