using SmartMenu.Services.ShoppingCartAPI.Models.Dto;
using SmartMenu.Services.ShoppingCartAPI.Service.IService;
using Newtonsoft.Json;

namespace SmartMenu.Services.ShoppingCartAPI.Service
{
    /// <summary>
    /// Provides methods for handling product-related operations.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class with the specified HTTP client factory.
        /// </summary>
        /// <param name="clientFactory">The HTTP client factory to be used for making HTTP requests.</param>
        public ProductService(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }

        /// <summary>
        /// Asynchronously retrieves the list of products.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of products.</returns>
        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var client = _httpClientFactory.CreateClient("Product");
            var response = await client.GetAsync($"/api/product");
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(resp.Result));
            }
            return new List<ProductDto>();
        }
    }
}
