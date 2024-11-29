namespace SmartMenu.Web.Models
{
    /// <summary>
    /// Data transfer object for the checkout process.
    /// </summary>
    public class CheckoutDto
    {
        /// <summary>
        /// Gets or sets the cart details.
        /// </summary>
        public CartDto CartDto { get; set; }

        /// <summary>
        /// Gets or sets the account details for editing.
        /// </summary>
        public EditAccountDto AccountDto { get; set; }
    }
}
