using SmartMenu.Web.Models;

namespace SmartMenu.Web.Service.IService
{
    public interface IRecipeService
    {
        Task<ResponseDto?> GetAllAsync();
        Task<ResponseDto?> GetByIdAsync(int id);
        Task<ResponseDto?> CreateAsync(RecipeDto recipeDto);
        Task<ResponseDto?> UpdateAsync(RecipeDto recipeDto);
        Task<ResponseDto?> DeleteAsync(int id);
    }
}
