using SmartMenu.Web.Models;

namespace SmartMenu.Web.Service.IService
{
    /// <summary>
    /// Defines methods for managing the shopping cart.
    /// </summary>
    public interface ICartService
    {
        /// <summary>
        /// Gets the cart by user ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> GetCartByUserIdAsnyc(string userId);

        /// <summary>
        /// Upserts the cart.
        /// </summary>
        /// <param name="cartDto">The cart data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> UpsertCartAsync(CartDto cartDto);

        /// <summary>
        /// Removes an item from the cart.
        /// </summary>
        /// <param name="cartDetailsId">The cart details ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> RemoveFromCartAsync(int cartDetailsId);

        /// <summary>
        /// Applies a coupon to the cart.
        /// </summary>
        /// <param name="cartDto">The cart data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto);

        /// <summary>
        /// Emails the cart.
        /// </summary>
        /// <param name="cartDto">The cart data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> EmailCart(CartDto cartDto);

        /// <summary>
        /// Clears the cart for a user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> ClearCartAsync(string userId);

        /// <summary>
        /// Increases the quantity of an item in the cart.
        /// </summary>
        /// <param name="cartDetailsId">The cart details ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> IncreaseQuantityAsync(int cartDetailsId);

        /// <summary>
        /// Decreases the quantity of an item in the cart.
        /// </summary>
        /// <param name="cartDetailsId">The cart details ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> DecreaseQuantityAsync(int cartDetailsId);
    }
}
