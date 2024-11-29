using SmartMenu.Web.Models;

namespace SmartMenu.Web.Service.IService
{
    /// <summary>
    /// Defines methods for managing orders.
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="cartDto">The cart data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> CreateOrder(CartDto cartDto);

        /// <summary>
        /// Creates a Stripe session for payment.
        /// </summary>
        /// <param name="stripeRequestDto">The Stripe request data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> CreateStripeSession(StripeRequestDto stripeRequestDto);

        /// <summary>
        /// Validates a Stripe session.
        /// </summary>
        /// <param name="orderHeaderId">The order header ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> ValidateStripeSession(int orderHeaderId);

        /// <summary>
        /// Gets all orders for a user.
        /// </summary>
        /// <param name="userId">The user ID (optional).</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> GetAllOrder(string? userId);

        /// <summary>
        /// Gets an order by its ID.
        /// </summary>
        /// <param name="orderId">The order ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> GetOrder(int orderId);

        /// <summary>
        /// Updates the status of an order.
        /// </summary>
        /// <param name="orderId">The order ID.</param>
        /// <param name="newStatus">The new status.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> UpdateOrderStatus(int orderId, string newStatus);
    }
}
