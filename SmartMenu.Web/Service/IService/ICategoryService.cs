using SmartMenu.Web.Models;

namespace SmartMenu.Web.Service.IService
{
    public interface ICategoryService
    {
     
        Task<ResponseDto?> GetAllCategoriesAsync();
        Task<ResponseDto?> GetCategoryByIdAsync(int id);
        Task<ResponseDto?> CreateCategoryAsync(CategoryDto categoryDto);
        Task<ResponseDto?> UpdateCategoryAsync(CategoryDto categoryDto);
        Task<ResponseDto?> DeleteCategoryAsync(int id);
    }
}
