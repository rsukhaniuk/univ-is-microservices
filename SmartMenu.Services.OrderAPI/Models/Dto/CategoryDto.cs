using System.ComponentModel.DataAnnotations;

namespace SmartMenu.Services.OrderAPI.Models.Dto
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}
