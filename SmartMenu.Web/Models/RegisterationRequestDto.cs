using System.ComponentModel.DataAnnotations;

namespace SmartMenu.Web.Models
{
    /// <summary>
    /// Data transfer object for registration requests.
    /// </summary>
    public class RegistrationRequestDto
    {
        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the user.
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the role of the user.
        /// </summary>
        public string? Role { get; set; }
    }
}
