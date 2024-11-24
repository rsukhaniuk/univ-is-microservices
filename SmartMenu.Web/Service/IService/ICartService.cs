using SmartMenu.Web.Models;

namespace SmartMenu.Web.Service.IService
{
    public interface ICartService
    {
        Task<ResponseDto?> GetCartByUserIdAsnyc(string userId);
        Task<ResponseDto?> UpsertCartAsync(CartDto cartDto);
        Task<ResponseDto?> RemoveFromCartAsync(int cartDetailsId);
        Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto);
        Task<ResponseDto?> EmailCart(CartDto cartDto);
        Task<ResponseDto?> ClearCartAsync(string userId);
        Task<ResponseDto?> IncreaseQuantity(string cartDetailsId);
        Task<ResponseDto?> DecreaseQuantity(string cartDetailsId);
    }
}
