using SmartMenu.Web.Models;
using SmartMenu.Web.Service.IService;
using SmartMenu.Web.Utility;

namespace SmartMenu.Web.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IBaseService _baseService;

        public CategoryService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        // Create a new category
        public async Task<ResponseDto?> CreateCategoryAsync(CategoryDto categoryDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = categoryDto,
                Url = SD.ProductAPIBase + "/api/category"
            });
        }

        // Get all categories
        public async Task<ResponseDto?> GetAllCategoriesAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/category"
            });
        }

        // Get a single category by ID
        public async Task<ResponseDto?> GetCategoryByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/category/" + id
            });
        }

        // Update an existing category
        public async Task<ResponseDto?> UpdateCategoryAsync(CategoryDto categoryDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = categoryDto,
                Url = SD.ProductAPIBase + "/api/category"
            });
        }

        // Delete a category
        public async Task<ResponseDto?> DeleteCategoryAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ProductAPIBase + "/api/category/" + id
            });
        }
    }
}
