using SmartMenu.Web.Models;

namespace SmartMenu.Web.Service.IService
{
    public interface ICategoryService
    {
     
        Task<ResponseDto?> GetAllCategoriesAsync();
        Task<ResponseDto?> GetCategoryByIdAsync(int id);
        Task<ResponseDto?> CreateCategoriesAsync(ProductDto productDto);
        Task<ResponseDto?> UpdateCategoriesAsync(ProductDto productDto);
        Task<ResponseDto?> DeleteCategoriesAsync(int id);
    }
}
