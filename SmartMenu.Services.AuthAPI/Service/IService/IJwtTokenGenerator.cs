using SmartMenu.Services.AuthAPI.Models;

namespace SmartMenu.Services.AuthAPI.Service.IService
{
    /// <summary>
    /// Interface for generating JWT tokens.
    /// </summary>
    public interface IJwtTokenGenerator
    {
        /// <summary>
        /// Generates a JWT token for the specified user and roles.
        /// </summary>
        /// <param name="applicationUser">The application user.</param>
        /// <param name="roles">The roles of the user.</param>
        /// <returns>The generated JWT token.</returns>
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}
