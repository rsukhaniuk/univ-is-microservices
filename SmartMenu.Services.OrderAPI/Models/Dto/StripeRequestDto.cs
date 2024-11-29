namespace SmartMenu.Services.OrderAPI.Models.Dto
{
    /// <summary>
    /// Represents a Stripe request.
    /// </summary>
    public class StripeRequestDto
    {
        /// <summary>
        /// Gets or sets the Stripe session URL.
        /// </summary>
        public string? StripeSessionUrl { get; set; }

        /// <summary>
        /// Gets or sets the Stripe session ID.
        /// </summary>
        public string? StripeSessionId { get; set; }

        /// <summary>
        /// Gets or sets the approved URL.
        /// </summary>
        public string ApprovedUrl { get; set; }

        /// <summary>
        /// Gets or sets the cancel URL.
        /// </summary>
        public string CancelUrl { get; set; }

        /// <summary>
        /// Gets or sets the order header.
        /// </summary>
        public OrderHeaderDto OrderHeader { get; set; }
    }
}
