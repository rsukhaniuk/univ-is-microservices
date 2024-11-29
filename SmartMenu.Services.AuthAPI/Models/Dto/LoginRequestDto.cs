namespace SmartMenu.Services.AuthAPI.Models.Dto
{
    /// <summary>
    /// Represents the data required for a login request.
    /// </summary>
    public class LoginRequestDto
    {
        /// <summary>
        /// Gets or sets the username for the login request.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password for the login request.
        /// </summary>
        public string Password { get; set; }
    }
}
