using SmartMenu.Web.Models;

namespace SmartMenu.Web.Service.IService
{
    /// <summary>
    /// Defines methods for managing coupons.
    /// </summary>
    public interface ICouponService
    {
        /// <summary>
        /// Gets a coupon by its code.
        /// </summary>
        /// <param name="couponCode">The coupon code.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> GetCouponAsync(string couponCode);

        /// <summary>
        /// Gets all coupons.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> GetAllCouponsAsync();

        /// <summary>
        /// Gets a coupon by its ID.
        /// </summary>
        /// <param name="id">The coupon ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> GetCouponByIdAsync(int id);

        /// <summary>
        /// Creates a new coupon.
        /// </summary>
        /// <param name="couponDto">The coupon data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> CreateCouponsAsync(CouponDto couponDto);

        /// <summary>
        /// Updates an existing coupon.
        /// </summary>
        /// <param name="couponDto">The coupon data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> UpdateCouponsAsync(CouponDto couponDto);

        /// <summary>
        /// Deletes a coupon by its ID.
        /// </summary>
        /// <param name="id">The coupon ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> DeleteCouponsAsync(int id);
    }
}
