using SmartMenu.Services.AuthAPI.Models.Dto;

namespace SmartMenu.Services.AuthAPI.Service.IService
{
    /// <summary>
    /// Interface for authentication-related services.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registrationRequestDto">The registration request data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a string indicating the result of the registration.</returns>
        Task<string> Register(RegistrationRequestDto registrationRequestDto);

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="loginRequestDto">The login request data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the login response data transfer object.</returns>
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

        /// <summary>
        /// Assigns a role to a user.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="roleName">The name of the role.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating success.</returns>
        Task<bool> AssignRole(string email, string roleName);

        /// <summary>
        /// Edits a user's account information.
        /// </summary>
        /// <param name="editAccountDto">The edit account data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating success.</returns>
        Task<bool> EditAccount(EditAccountDto editAccountDto);

        /// <summary>
        /// Deletes a user's account.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating success.</returns>
        Task<bool> DeleteAccount(string email);

        /// <summary>
        /// Changes a user's password.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="changePasswordDto">The change password data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating success.</returns>
        Task<bool> ChangePassword(string userId, ChangePasswordDto changePasswordDto);

        /// <summary>
        /// Gets the details of a user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the edit account data transfer object.</returns>
        Task<EditAccountDto?> GetUserDetailsAsync(string userId);
    }
}
