using System.ComponentModel.DataAnnotations;

namespace SmartMenu.Services.ShoppingCartAPI.Models.Dto
{
    /// <summary>
    /// Represents a data transfer object for a category.
    /// </summary>
    public class CategoryDto
    {
        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        public string Name { get; set; }
    }
}
