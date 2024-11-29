using System.ComponentModel.DataAnnotations;

namespace SmartMenu.Services.ShoppingCartAPI.Models
{
    /// <summary>
    /// Represents a discount coupon.
    /// </summary>
    public class Coupon
    {
        /// <summary>
        /// Gets or sets the coupon identifier.
        /// </summary>
        [Key]
        public int CouponId { get; set; }

        /// <summary>
        /// Gets or sets the coupon code.
        /// </summary>
        [Required]
        public string CouponCode { get; set; }

        /// <summary>
        /// Gets or sets the discount amount.
        /// </summary>
        [Required]
        public double DiscountAmount { get; set; }

        /// <summary>
        /// Gets or sets the minimum amount required to use the coupon.
        /// </summary>
        public int MinAmount { get; set; }
    }
}
