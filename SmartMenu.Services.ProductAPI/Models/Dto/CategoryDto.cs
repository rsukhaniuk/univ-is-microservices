namespace SmartMenu.Services.ProductAPI.Models.Dto
{
    /// <summary>
    /// Represents a data transfer object for a category.
    /// </summary>
    public class CategoryDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the category.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        public string Name { get; set; }
    }
}
