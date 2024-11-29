using SmartMenu.Web.Models;

namespace SmartMenu.Web.Service.IService
{
    /// <summary>
    /// Defines methods for managing categories.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Gets all categories.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> GetAllCategoriesAsync();

        /// <summary>
        /// Gets a category by its ID.
        /// </summary>
        /// <param name="id">The category ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> GetCategoryByIdAsync(int id);

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="categoryDto">The category data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> CreateCategoryAsync(CategoryDto categoryDto);

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="categoryDto">The category data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> UpdateCategoryAsync(CategoryDto categoryDto);

        /// <summary>
        /// Deletes a category by its ID.
        /// </summary>
        /// <param name="id">The category ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> DeleteCategoryAsync(int id);
    }
}
