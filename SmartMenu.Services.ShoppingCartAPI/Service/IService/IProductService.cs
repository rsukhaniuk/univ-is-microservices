using SmartMenu.Services.ShoppingCartAPI.Models.Dto;

namespace SmartMenu.Services.ShoppingCartAPI.Service.IService
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
