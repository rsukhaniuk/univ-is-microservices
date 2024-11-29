using SmartMenu.Web.Models;

namespace SmartMenu.Web.Service.IService
{
    /// <summary>
    /// Defines methods for authentication and user management services.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="loginRequestDto">The login request data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registrationRequestDto">The registration request data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto);

        /// <summary>
        /// Assigns a role to a user.
        /// </summary>
        /// <param name="registrationRequestDto">The registration request data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto);

        /// <summary>
        /// Edits a user's account details.
        /// </summary>
        /// <param name="editAccountDto">The edit account data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> EditAccountAsync(EditAccountDto editAccountDto);

        /// <summary>
        /// Changes a user's password.
        /// </summary>
        /// <param name="changePasswordDto">The change password data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> ChangePasswordAsync(ChangePasswordDto changePasswordDto);

        /// <summary>
        /// Deletes a user's account.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> DeleteAccountAsync(string userId);

        /// <summary>
        /// Gets the details of a user by ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> GetUserDetailsAsync(string userId);
    }
}
