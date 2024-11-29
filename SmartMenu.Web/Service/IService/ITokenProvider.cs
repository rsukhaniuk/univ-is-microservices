namespace SmartMenu.Web.Service.IService
{
    /// <summary>
    /// Defines methods for managing authentication tokens.
    /// </summary>
    public interface ITokenProvider
    {
        /// <summary>
        /// Sets the authentication token.
        /// </summary>
        /// <param name="token">The authentication token.</param>
        void SetToken(string token);

        /// <summary>
        /// Gets the authentication token.
        /// </summary>
        /// <returns>The authentication token, or null if no token is set.</returns>
        string? GetToken();

        /// <summary>
        /// Clears the authentication token.
        /// </summary>
        void ClearToken();
    }
}
