using SmartMenu.Web.Models;

namespace SmartMenu.Web.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto);
        Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto);
        Task<ResponseDto?> EditAccountAsync(EditAccountDto editAccountDto);
        Task<ResponseDto?> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
        Task<ResponseDto?> DeleteAccountAsync(string userId);
        Task<ResponseDto?> GetUserDetailsAsync(string userId); 
    }
}
