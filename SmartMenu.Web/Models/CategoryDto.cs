using SmartMenu.Web.Utility;
using System.ComponentModel.DataAnnotations;

namespace SmartMenu.Web.Models
{
    /// <summary>
    /// Represents a category.
    /// </summary>
    public class CategoryDto
    {
        /// <summary>
        /// Gets or sets the category ID.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        public string Name { get; set; }
    }
}
