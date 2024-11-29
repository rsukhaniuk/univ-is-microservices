using SmartMenu.Web.Models;
using SmartMenu.Web.Service.IService;
using SmartMenu.Web.Utility;

namespace SmartMenu.Web.Service
{
    /// <summary>
    /// Provides services related to orders.
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly IBaseService _baseService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService"/> class.
        /// </summary>
        /// <param name="baseService">The base service to be used for sending requests.</param>
        public OrderService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="cartDto">The cart data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        public async Task<ResponseDto?> CreateOrder(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.OrderAPIBase + "/api/order/CreateOrder"
            });
        }

        /// <summary>
        /// Creates a new Stripe session.
        /// </summary>
        /// <param name="stripeRequestDto">The Stripe request data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        public async Task<ResponseDto?> CreateStripeSession(StripeRequestDto stripeRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = stripeRequestDto,
                Url = SD.OrderAPIBase + "/api/order/CreateStripeSession"
            });
        }

        /// <summary>
        /// Gets all orders for a specific user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        public async Task<ResponseDto?> GetAllOrder(string? userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.OrderAPIBase + "/api/order/GetOrders?userId=" + userId
            });
        }

        /// <summary>
        /// Gets a specific order by ID.
        /// </summary>
        /// <param name="orderId">The order ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        public async Task<ResponseDto?> GetOrder(int orderId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.OrderAPIBase + "/api/order/GetOrder/" + orderId
            });
        }

        /// <summary>
        /// Updates the status of an order.
        /// </summary>
        /// <param name="orderId">The order ID.</param>
        /// <param name="newStatus">The new status of the order.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        public async Task<ResponseDto?> UpdateOrderStatus(int orderId, string newStatus)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = newStatus,
                Url = SD.OrderAPIBase + "/api/order/UpdateOrderStatus/" + orderId
            });
        }

        /// <summary>
        /// Validates a Stripe session.
        /// </summary>
        /// <param name="orderHeaderId">The order header ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        public async Task<ResponseDto?> ValidateStripeSession(int orderHeaderId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = orderHeaderId,
                Url = SD.OrderAPIBase + "/api/order/ValidateStripeSession"
            });
        }
    }
}
