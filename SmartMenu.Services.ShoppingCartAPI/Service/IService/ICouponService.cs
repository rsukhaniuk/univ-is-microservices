using SmartMenu.Services.ShoppingCartAPI.Models.Dto;

namespace SmartMenu.Services.ShoppingCartAPI.Service.IService
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon(string couponCode);
    }
}
