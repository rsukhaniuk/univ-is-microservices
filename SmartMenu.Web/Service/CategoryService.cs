using SmartMenu.Web.Models;
using SmartMenu.Web.Service.IService;
using SmartMenu.Web.Utility;

namespace SmartMenu.Web.Service
{
    /// <summary>
    /// Provides services related to categories.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly IBaseService _baseService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryService"/> class.
        /// </summary>
        /// <param name="baseService">The base service to be used for sending requests.</param>
        public CategoryService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="categoryDto">The category data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        public async Task<ResponseDto?> CreateCategoryAsync(CategoryDto categoryDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = categoryDto,
                Url = SD.ProductAPIBase + "/api/category"
            });
        }

        /// <summary>
        /// Gets all categories.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        public async Task<ResponseDto?> GetAllCategoriesAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/category"
            });
        }

        /// <summary>
        /// Gets a single category by ID.
        /// </summary>
        /// <param name="id">The category ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        public async Task<ResponseDto?> GetCategoryByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/category/" + id
            });
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="categoryDto">The category data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        public async Task<ResponseDto?> UpdateCategoryAsync(CategoryDto categoryDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = categoryDto,
                Url = SD.ProductAPIBase + "/api/category"
            });
        }

        /// <summary>
        /// Deletes a category.
        /// </summary>
        /// <param name="id">The category ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
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
