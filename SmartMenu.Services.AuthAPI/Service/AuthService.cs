using SmartMenu.Services.AuthAPI.Data;
using SmartMenu.Services.AuthAPI.Models;
using SmartMenu.Services.AuthAPI.Models.Dto;
using SmartMenu.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace SmartMenu.Services.AuthAPI.Service
{
    /// <summary>
    /// Service for handling authentication-related operations.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="db">The database context.</param>
        /// <param name="jwtTokenGenerator">The JWT token generator.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The role manager.</param>
        public AuthService(AppDbContext db, IJwtTokenGenerator jwtTokenGenerator,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Assigns a role to a user.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="roleName">The name of the role.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating success.</returns>
        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    // Create role if it does not exist
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="loginRequestDto">The login request data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the login response data transfer object.</returns>
        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user == null || isValid == false)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }

            // If user was found, generate JWT token
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            UserDto userDTO = new()
            {
                Email = user.Email,
                ID = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDTO,
                Token = token
            };

            return loginResponseDto;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registrationRequestDto">The registration request data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a string indicating the result of the registration.</returns>
        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                Name = registrationRequestDto.Name,
                PhoneNumber = registrationRequestDto.PhoneNumber
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _db.ApplicationUsers.First(u => u.UserName == registrationRequestDto.Email);

                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        ID = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber
                    };

                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }
            return "Error Encountered";
        }

        /// <summary>
        /// Edits a user's account information.
        /// </summary>
        /// <param name="editAccountDto">The edit account data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating success.</returns>
        public async Task<bool> EditAccount(EditAccountDto editAccountDto)
        {
            var user = await _userManager.FindByIdAsync(editAccountDto.UserId);
            if (user == null) return false;

            // Update name if provided
            if (!string.IsNullOrEmpty(editAccountDto.NewName))
                user.Name = editAccountDto.NewName;

            // Update phone number if provided
            if (!string.IsNullOrEmpty(editAccountDto.NewPhoneNumber))
                user.PhoneNumber = editAccountDto.NewPhoneNumber;

            // Update email if provided
            if (!string.IsNullOrEmpty(editAccountDto.NewEmail) && !editAccountDto.NewEmail.Equals(user.Email, StringComparison.OrdinalIgnoreCase))
            {
                // Check if the new email already exists
                var existingUser = await _userManager.FindByEmailAsync(editAccountDto.NewEmail);
                if (existingUser != null)
                {
                    return false; // Email is already taken
                }

                user.Email = editAccountDto.NewEmail;
                user.NormalizedEmail = editAccountDto.NewEmail.ToUpper();
                user.UserName = editAccountDto.NewEmail; // Optional: Update UserName if it mirrors Email
            }

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        /// <summary>
        /// Deletes a user's account.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating success.</returns>
        public async Task<bool> DeleteAccount(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            // Perform the account deletion
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        /// <summary>
        /// Changes a user's password.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="changePasswordDto">The change password data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating success.</returns>
        public async Task<bool> ChangePassword(string userId, ChangePasswordDto changePasswordDto)
        {
            // Ensure the new password matches the confirmation password
            if (changePasswordDto.NewPassword != changePasswordDto.ConfirmNewPassword)
            {
                return false;
            }

            // Find the user by ID
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            // Verify and update the password
            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            return result.Succeeded;
        }

        /// <summary>
        /// Gets the details of a user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the edit account data transfer object.</returns>
        public async Task<EditAccountDto?> GetUserDetailsAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return null;

            return new EditAccountDto
            {
                UserId = user.Id,
                NewName = user.Name,
                NewEmail = user.Email,
                NewPhoneNumber = user.PhoneNumber
            };
        }
    }
}
