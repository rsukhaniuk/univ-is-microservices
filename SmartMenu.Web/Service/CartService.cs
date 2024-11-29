using SmartMenu.Web.Models;
using SmartMenu.Web.Service.IService;
using SmartMenu.Web.Utility;
using Stripe.Climate;

namespace SmartMenu.Web.Service
{
    /// <summary>
    /// Provides services related to the shopping cart.
    /// </summary>
    public class CartService : ICartService
    {
        private readonly IBaseService _baseService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartService"/> class.
        /// </summary>
        /// <param name="baseService">The base service to be used for sending requests.</param>
        public CartService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// Applies a coupon to the cart.
        /// </summary>
        /// <param name="cartDto">The cart data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        public async Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "/api/cart/ApplyCoupon"
            });
        }

        /// <summary>
        /// Sends an email with the cart details.
        /// </summary>
        /// <param name="cartDto">The cart data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        public async Task<ResponseDto?> EmailCart(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "/api/cart/EmailCartRequest"
            });
        }

        /// <summary>
        /// Gets the cart details by user ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        public async Task<ResponseDto?> GetCartByUserIdAsnyc(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ShoppingCartAPIBase + "/api/cart/GetCart/" + userId
            });
        }

        /// <summary>
        /// Removes an item from the cart.
        /// </summary>
        /// <param name="cartDetailsId">The cart details ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        public async Task<ResponseDto?> RemoveFromCartAsync(int cartDetailsId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDetailsId,
                Url = SD.ShoppingCartAPIBase + "/api/cart/RemoveCart"
            });
        }

        /// <summary>
        /// Adds or updates the cart.
        /// </summary>
        /// <param name="cartDto">The cart data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        public async Task<ResponseDto?> UpsertCartAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "/api/cart/CartUpsert"
            });
        }

        /// <summary>
        /// Clears the cart for a user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        public async Task<ResponseDto?> ClearCartAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ShoppingCartAPIBase + "/api/cart/ClearCart/" + userId
            });
        }

        /// <summary>
        /// Increases the quantity of an item in the cart.
        /// </summary>
        /// <param name="cartDetailsId">The cart details ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        public async Task<ResponseDto?> IncreaseQuantityAsync(int cartDetailsId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ShoppingCartAPIBase + "/api/cart/IncreaseQuantity/" + cartDetailsId
            });
        }

        /// <summary>
        /// Decreases the quantity of an item in the cart.
        /// </summary>
        /// <param name="cartDetailsId">The cart details ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        public async Task<ResponseDto?> DecreaseQuantityAsync(int cartDetailsId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ShoppingCartAPIBase + "/api/cart/DecreaseQuantity/" + cartDetailsId
            });
        }
    }
}
