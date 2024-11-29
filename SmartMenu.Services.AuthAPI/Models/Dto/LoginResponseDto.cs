namespace SmartMenu.Services.AuthAPI.Models.Dto
{
    /// <summary>
    /// Represents the response data for a login request.
    /// </summary>
    public class LoginResponseDto
    {
        /// <summary>
        /// Gets or sets the user information.
        /// </summary>
        public UserDto User { get; set; }

        /// <summary>
        /// Gets or sets the JWT token.
        /// </summary>
        public string Token { get; set; }
    }
}
