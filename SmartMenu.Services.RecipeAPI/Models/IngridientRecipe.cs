using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using static SmartMenu.Services.RecipeAPI.Utility.SD;

namespace SmartMenu.Services.RecipeAPI.Models
{
    public class IngridientRecipe
    {
        [Key]
        public int IngridientRecipeId { get; set; }

        [DisplayName("Recipe")]
        public int RecipeId { get; set; }

        [ForeignKey("RecipeId")]
        [ValidateNever]
        public Recipe Recipe { get; set; }

        [DisplayName("Ingredient")]
        public int IngredientId { get; set; }

        [ForeignKey("IngredientId")]
        [ValidateNever]
        public Ingredient Ingredient { get; set; }

        [Required]
        public double Quantity { get; set; }

        [Required]
        public MeasurementUnit Unit { get; set; }


    }
}
