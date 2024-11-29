namespace SmartMenu.Services.ShoppingCartAPI.Models.Dto
{
    /// <summary>
    /// Represents a data transfer object for a coupon.
    /// </summary>
    public class CouponDto
    {
        /// <summary>
        /// Gets or sets the coupon identifier.
        /// </summary>
        public int CouponId { get; set; }

        /// <summary>
        /// Gets or sets the coupon code.
        /// </summary>
        public string CouponCode { get; set; }

        /// <summary>
        /// Gets or sets the discount amount.
        /// </summary>
        public double DiscountAmount { get; set; }

        /// <summary>
        /// Gets or sets the minimum amount required to use the coupon.
        /// </summary>
        public int MinAmount { get; set; }
    }
}
