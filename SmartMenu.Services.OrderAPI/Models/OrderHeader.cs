using System.ComponentModel.DataAnnotations;

namespace SmartMenu.Services.OrderAPI.Models
{
    /// <summary>
    /// Represents the header of an order.
    /// </summary>
    public class OrderHeader
    {
        /// <summary>
        /// Gets or sets the order header ID.
        /// </summary>
        [Key]
        public int OrderHeaderId { get; set; }

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
        /// Gets or sets the total amount of the order.
        /// </summary>
        public double OrderTotal { get; set; }

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

        /// <summary>
        /// Gets or sets the time of the order.
        /// </summary>
        public DateTime OrderTime { get; set; }

        /// <summary>
        /// Gets or sets the status of the order.
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets the payment intent ID.
        /// </summary>
        public string? PaymentIntentId { get; set; }

        /// <summary>
        /// Gets or sets the Stripe session ID.
        /// </summary>
        public string? StripeSessionId { get; set; }

        /// <summary>
        /// Gets or sets the order details.
        /// </summary>
        public IEnumerable<OrderDetails> OrderDetails { get; set; }
    }
}
