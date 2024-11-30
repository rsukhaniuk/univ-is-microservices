using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMenu.Services.ShoppingCartAPI.Models
{
    /// <summary>
    /// Represents the header information of a shopping cart.
    /// </summary>
    public class CartHeader
    {
        /// <summary>
        /// Gets or sets the unique identifier for the cart header.
        /// </summary>
        [Key]
        public int CartHeaderId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// Gets or sets the coupon code applied to the cart.
        /// </summary>
        public string? CouponCode { get; set; }

        /// <summary>
        /// Gets or sets the discount amount applied to the cart.
        /// </summary>
        [NotMapped]
        public double Discount { get; set; }

        /// <summary>
        /// Gets or sets the total amount of the cart.
        /// </summary>
        [NotMapped]
        public double CartTotal { get; set; }
    }
}
