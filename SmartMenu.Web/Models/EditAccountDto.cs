namespace SmartMenu.Web.Models
{
    /// <summary>
    /// Data transfer object for editing account details.
    /// </summary>
    public class EditAccountDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        public string UserId { get; set; } // The unique identifier of the user

        /// <summary>
        /// Gets or sets the new name to update.
        /// </summary>
        public string? NewName { get; set; }

        /// <summary>
        /// Gets or sets the new phone number to update.
        /// </summary>
        public string? NewPhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the new email address to update.
        /// </summary>
        public string? NewEmail { get; set; } // New email address to update
    }
}
