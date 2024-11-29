namespace SmartMenu.Services.AuthAPI.Models
{
    /// <summary>
    /// Represents the options for JWT authentication.
    /// </summary>
    public class JwtOptions
    {
        /// <summary>
        /// Gets or sets the issuer of the JWT.
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the audience of the JWT.
        /// </summary>
        public string Audience { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the secret key used for signing the JWT.
        /// </summary>
        public string Secret { get; set; } = string.Empty;
    }
}
