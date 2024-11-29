using System.ComponentModel.DataAnnotations;

namespace SmartMenu.Web.Models
{
    /// <summary>
    /// Data transfer object for login requests.
    /// </summary>
    public class LoginRequestDto
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
