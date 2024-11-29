using Microsoft.AspNetCore.Identity;

namespace SmartMenu.Services.AuthAPI.Models
{
    /// <summary>
    /// Represents an application user with additional properties.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string Name { get; set; }
    }
}
