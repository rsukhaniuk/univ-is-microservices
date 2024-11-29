using SmartMenu.Web.Models;

namespace SmartMenu.Web.Service.IService
{
    /// <summary>
    /// Defines methods for managing products.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> GetAllProductsAsync();

        /// <summary>
        /// Gets a product by its ID.
        /// </summary>
        /// <param name="id">The product ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> GetProductByIdAsync(int id);

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="productDto">The product data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> CreateProductsAsync(ProductDto productDto);

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="productDto">The product data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> UpdateProductsAsync(ProductDto productDto);

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The product ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> DeleteProductsAsync(int id);
    }
}
