namespace SmartMenu.Services.AuthAPI.Models.Dto
{
    /// <summary>
    /// Represents the data required to edit a user's account information.
    /// </summary>
    public class EditAccountDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        public string UserId { get; set; } // The unique identifier of the user

        /// <summary>
        /// Gets or sets the new name of the user.
        /// </summary>
        public string? NewName { get; set; }

        /// <summary>
        /// Gets or sets the new phone number of the user.
        /// </summary>
        public string? NewPhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the new email address to update.
        /// </summary>
        public string? NewEmail { get; set; } // New email address to update
    }
}
