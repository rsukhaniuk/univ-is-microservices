namespace SmartMenu.Web.Models
{
    /// <summary>
    /// Data transfer object for user details.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the user.
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}
