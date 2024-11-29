using SmartMenu.Services.ShoppingCartAPI.Models.Dto;

namespace SmartMenu.Services.ShoppingCartAPI.Service.IService
{
    /// <summary>
    /// Interface for coupon service operations.
    /// </summary>
    public interface ICouponService
    {
        /// <summary>
        /// Gets the coupon details by coupon code.
        /// </summary>
        /// <param name="couponCode">The coupon code.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the coupon details.</returns>
        Task<CouponDto> GetCoupon(string couponCode);
    }
}
