namespace SmartMenu.Web.Models
{
    /// <summary>
    /// Data transfer object for Stripe payment requests.
    /// </summary>
    public class StripeRequestDto
    {
        /// <summary>
        /// Gets or sets the Stripe session URL.
        /// </summary>
        public string? StripeSessionUrl { get; set; }

        /// <summary>
        /// Gets or sets the Stripe session identifier.
        /// </summary>
        public string? StripeSessionId { get; set; }

        /// <summary>
        /// Gets or sets the URL to redirect to upon approval.
        /// </summary>
        public string ApprovedUrl { get; set; }

        /// <summary>
        /// Gets or sets the URL to redirect to upon cancellation.
        /// </summary>
        public string CancelUrl { get; set; }

        /// <summary>
        /// Gets or sets the order header details.
        /// </summary>
        public OrderHeaderDto OrderHeader { get; set; }
    }
}
