namespace SmartMenu.Web.Models
{
    /// <summary>
    /// Data transfer object for login responses.
    /// </summary>
    public class LoginResponseDto
    {
        /// <summary>
        /// Gets or sets the user details.
        /// </summary>
        public UserDto User { get; set; }

        /// <summary>
        /// Gets or sets the authentication token.
        /// </summary>
        public string Token { get; set; }
    }
}
