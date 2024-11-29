using SmartMenu.Web.Models;
using SmartMenu.Web.Service.IService;
using SmartMenu.Web.Utility;

namespace SmartMenu.Web.Service
{
    /// <summary>
    /// Provides authentication-related services.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="baseService">The base service to be used for sending requests.</param>
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// Assigns a role to a user.
        /// </summary>
        /// <param name="registrationRequestDto">The registration request data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = registrationRequestDto,
                Url = SD.AuthAPIBase + "/api/auth/AssignRole"
            });
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="loginRequestDto">The login request data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = loginRequestDto,
                Url = SD.AuthAPIBase + "/api/auth/login"
            }, withBearer: false);
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registrationRequestDto">The registration request data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = registrationRequestDto,
                Url = SD.AuthAPIBase + "/api/auth/register"
            }, withBearer: false);
        }

        /// <summary>
        /// Edits a user's account information.
        /// </summary>
        /// <param name="editAccountDto">The edit account data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        public async Task<ResponseDto?> EditAccountAsync(EditAccountDto editAccountDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = editAccountDto,
                Url = SD.AuthAPIBase + "/api/auth/EditAccount"
            });
        }

        /// <summary>
        /// Changes a user's password.
        /// </summary>
        /// <param name="changePasswordDto">The change password data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        public async Task<ResponseDto?> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = changePasswordDto,
                Url = SD.AuthAPIBase + "/api/auth/ChangePassword"
            });
        }

        /// <summary>
        /// Deletes a user's account.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        public async Task<ResponseDto?> DeleteAccountAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.AuthAPIBase}/api/auth/DeleteAccount/{userId}"
            });
        }

        /// <summary>
        /// Gets the details of a user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        public async Task<ResponseDto?> GetUserDetailsAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.AuthAPIBase}/api/auth/GetUserDetails/{userId}"
            });
        }
    }
}
