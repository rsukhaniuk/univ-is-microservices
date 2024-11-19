using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SmartMenu.Services.RecipeAPI.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
