using SmartMenu.Web.Models;

namespace SmartMenu.Web.Service.IService
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetAsync(string couponCode);
        Task<ResponseDto?> GetAllAsync();
        Task<ResponseDto?> GetByIdAsync(int id);
        Task<ResponseDto?> CreateAsync(CouponDto couponDto);
        Task<ResponseDto?> UpdateAsync(CouponDto couponDto);
        Task<ResponseDto?> DeleteAsync(int id);
    }
}
