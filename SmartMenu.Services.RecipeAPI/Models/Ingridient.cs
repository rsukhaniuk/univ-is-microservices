using System.ComponentModel.DataAnnotations;

namespace SmartMenu.Services.RecipeAPI.Models
{
    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
