namespace SmartMenu.Services.AuthAPI.Models.Dto
{
    /// <summary>
    /// Represents the data required to change a user's password.
    /// </summary>
    public class ChangePasswordDto
    {
        /// <summary>
        /// Gets or sets the user's current password.
        /// </summary>
        public string CurrentPassword { get; set; } // The user's current password

        /// <summary>
        /// Gets or sets the new password.
        /// </summary>
        public string NewPassword { get; set; } // The new password

        /// <summary>
        /// Gets or sets the confirmation of the new password.
        /// </summary>
        public string ConfirmNewPassword { get; set; } // Confirm the new password
    }
}
