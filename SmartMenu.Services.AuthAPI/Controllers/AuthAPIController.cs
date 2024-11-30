using SmartMenu.Services.AuthAPI.Models.Dto;
using SmartMenu.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SmartMenu.Services.AuthAPI.Models;

namespace SmartMenu.Services.AuthAPI.Controllers
{
    /// <summary>
    /// Handles authentication-related operations.
    /// </summary>
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _response;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthAPIController"/> class.
        /// </summary>
        /// <param name="authService">The authentication service.</param>
        /// <param name="configuration">The configuration settings.</param>
        public AuthAPIController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _response = new();
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="model">The registration request data.</param>
        /// <returns>The result of the operation.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            var errorMessage = await _authService.Register(model);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="model">The login request data.</param>
        /// <returns>The result of the operation.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await _authService.Login(model);
            if (loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Username or password is incorrect";
                return BadRequest(_response);
            }
            _response.Result = loginResponse;
            return Ok(_response);
        }

        /// <summary>
        /// Assigns a role to a user.
        /// </summary>
        /// <param name="model">The registration request data.</param>
        /// <returns>The result of the operation.</returns>
        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.Role.ToUpper());
            if (!assignRoleSuccessful)
            {
                _response.IsSuccess = false;
                _response.Message = "Error encountered";
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        /// <summary>
        /// Edits the account details of the logged-in user.
        /// </summary>
        /// <param name="model">The account edit data.</param>
        /// <returns>The result of the operation.</returns>
        [Authorize]
        [HttpPut("EditAccount")]
        public async Task<IActionResult> EditAccount([FromBody] EditAccountDto model)
        {
            try
            {
                var loggedInUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(loggedInUserId) || !loggedInUserId.Equals(model.UserId, StringComparison.OrdinalIgnoreCase))
                {
                    _response.IsSuccess = false;
                    _response.Message = "You are not authorized to edit this account.";
                    return Unauthorized(_response);
                }

                var editSuccessful = await _authService.EditAccount(model);
                if (!editSuccessful)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Failed to update the account. Please check the details.";
                    return BadRequest(_response);
                }

                _response.Message = "Account updated successfully.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(500, _response);
            }
        }

        /// <summary>
        /// Deletes the account of the logged-in user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>The result of the operation.</returns>
        [Authorize]
        [HttpDelete("DeleteAccount/{userId}")]
        public async Task<IActionResult> DeleteAccount(string userId)
        {
            try
            {
                var loggedInUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(loggedInUserId) || !loggedInUserId.Equals(userId, StringComparison.OrdinalIgnoreCase))
                {
                    _response.IsSuccess = false;
                    _response.Message = "You are not authorized to delete this account.";
                    return Unauthorized(_response);
                }

                var deleteSuccessful = await _authService.DeleteAccount(userId);
                if (!deleteSuccessful)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Failed to delete the account. The user may not exist.";
                    return BadRequest(_response);
                }

                _response.Message = "Account deleted successfully.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(500, _response);
            }
        }

        /// <summary>
        /// Changes the password of the logged-in user.
        /// </summary>
        /// <param name="model">The password change data.</param>
        /// <returns>The result of the operation.</returns>
        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto model)
        {
            try
            {
                var loggedInUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(loggedInUserId))
                {
                    _response.IsSuccess = false;
                    _response.Message = "Unable to identify the logged-in user.";
                    return Unauthorized(_response);
                }

                var changeSuccessful = await _authService.ChangePassword(loggedInUserId, model);
                if (!changeSuccessful)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Failed to change the password. Please check the details.";
                    return BadRequest(_response);
                }

                _response.Message = "Password changed successfully.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(500, _response);
            }
        }

        /// <summary>
        /// Gets the details of the logged-in user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>The result of the operation.</returns>
        [Authorize]
        [HttpGet("GetUserDetails/{userId}")]
        public async Task<IActionResult> GetUserDetails(string userId)
        {
            try
            {
                var loggedInUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(loggedInUserId) || !loggedInUserId.Equals(userId, StringComparison.OrdinalIgnoreCase))
                {
                    _response.IsSuccess = false;
                    _response.Message = "You are not authorized to view this account.";
                    return Unauthorized(_response);
                }

                var userDetails = await _authService.GetUserDetailsAsync(userId);

                if (userDetails == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "User not found.";
                    return NotFound(_response);
                }

                _response.Result = userDetails;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(500, _response);
            }
        }
    }
}
