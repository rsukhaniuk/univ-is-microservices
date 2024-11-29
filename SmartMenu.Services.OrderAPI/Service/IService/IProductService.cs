
using SmartMenu.Services.OrderAPI.Models.Dto;

namespace SmartMenu.Services.OrderAPI.Service.IService
{
    /// <summary>
    /// Interface for product service operations.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Gets the list of products.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of products.</returns>
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
