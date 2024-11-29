namespace SmartMenu.Web.Models
{
    /// <summary>
    /// Data transfer object for order header details.
    /// </summary>
    public class OrderHeaderDto
    {
        /// <summary>
        /// Gets or sets the order header identifier.
        /// </summary>
        public int OrderHeaderId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
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
        /// Gets or sets the total order amount.
        /// </summary>
        public double OrderTotal { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the customer.
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Gets or sets the email address of the customer.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the order time.
        /// </summary>
        public DateTime OrderTime { get; set; }

        /// <summary>
        /// Gets or sets the status of the order.
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets the payment intent identifier.
        /// </summary>
        public string? PaymentIntentId { get; set; }

        /// <summary>
        /// Gets or sets the Stripe session identifier.
        /// </summary>
        public string? StripeSessionId { get; set; }

        /// <summary>
        /// Gets or sets the order details.
        /// </summary>
        public IEnumerable<OrderDetailsDto> OrderDetails { get; set; }
    }
}
