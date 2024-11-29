using System.ComponentModel.DataAnnotations;

namespace SmartMenu.Services.ProductAPI.Models
{
    /// <summary>
    /// Represents a category in the product API.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Gets or sets the unique identifier for the category.
        /// </summary>
        [Key]
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
