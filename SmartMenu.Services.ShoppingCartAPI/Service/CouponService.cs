using SmartMenu.Services.ShoppingCartAPI.Models.Dto;
using SmartMenu.Services.ShoppingCartAPI.Service.IService;
using Newtonsoft.Json;

namespace SmartMenu.Services.ShoppingCartAPI.Service
{
    /// <summary>
    /// Provides methods for interacting with the coupon service.
    /// </summary>
    public class CouponService : ICouponService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CouponService"/> class with the specified HTTP client factory.
        /// </summary>
        /// <param name="clientFactory">The HTTP client factory to be used for making HTTP requests.</param>
        public CouponService(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }

        /// <summary>
        /// Asynchronously retrieves the coupon details by the specified coupon code.
        /// </summary>
        /// <param name="couponCode">The code of the coupon to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the coupon details.</returns>
        public async Task<CouponDto> GetCoupon(string couponCode)
        {
            var client = _httpClientFactory.CreateClient("Coupon");
            var response = await client.GetAsync($"/api/coupon/GetByCode/{couponCode}");
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            if (resp != null && resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(resp.Result));
            }
            return new CouponDto();
        }
    }
}
