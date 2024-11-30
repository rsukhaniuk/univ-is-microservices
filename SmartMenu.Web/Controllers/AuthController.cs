using SmartMenu.Web.Models;
using SmartMenu.Web.Service.IService;
using SmartMenu.Web.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace SmartMenu.Web.Controllers
{
    /// <summary>
    /// Controller for handling user authentication and account management in the web-application.
    /// </summary>
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;

        /// <summary>
        /// Constructor for the AuthController.
        /// </summary>
        /// <param name="authService">The authentication service.</param>
        /// <param name="tokenProvider">The token provider.</param>
        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }

        /// <summary>
        /// Get-method for the Login view.
        /// </summary>
        /// <returns>Returns the Login view with a new LoginRequestDto object.</returns>
        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }

        /// <summary>
        /// Post-method for the Login view.
        /// </summary>
        /// <param name="obj">LoginRequestDto that contains the user's login information.</param>
        /// <returns>Returns the Login view with a new LoginRequestDto object.</returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto obj)
        {
            ResponseDto responseDto = await _authService.LoginAsync(obj);

            if (responseDto != null && responseDto.IsSuccess)
            {
                LoginResponseDto loginResponseDto =
                    JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));

                await SignInUser(loginResponseDto);
                _tokenProvider.SetToken(loginResponseDto.Token);
                TempData["success"] = "Login Successful";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = responseDto.Message;
                return View(obj);
            }
        }

        /// <summary>
        /// Get-method for the Register view.
        /// </summary>
        /// <returns>Returns the Register view with a new RegistrationRequestDto object.</returns>
        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
                {
                    new SelectListItem{Text=SD.RoleAdmin,Value=SD.RoleAdmin},
                    new SelectListItem{Text=SD.RoleCustomer,Value=SD.RoleCustomer},
                };

            ViewBag.RoleList = roleList;
            return View();
        }

        /// <summary>
        /// Post-method for the Register view.
        /// </summary>
        /// <param name="obj">RegistrationRequestDto that contains the user's registration information.</param>
        /// <returns>Returns the Register view with a new RegistrationRequestDto object.</returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto obj)
        {
            ResponseDto result = await _authService.RegisterAsync(obj);
            ResponseDto assingRole;

            if (result != null && result.IsSuccess)
            {
                if (string.IsNullOrEmpty(obj.Role))
                {
                    obj.Role = SD.RoleCustomer;
                }
                assingRole = await _authService.AssignRoleAsync(obj);
                if (assingRole != null && assingRole.IsSuccess)
                {
                    TempData["success"] = "Registration Successful";
                    return RedirectToAction(nameof(Login));
                }
            }
            else
            {
                TempData["error"] = result.Message;
            }

            var roleList = new List<SelectListItem>()
                {
                    new SelectListItem{Text=SD.RoleAdmin,Value=SD.RoleAdmin},
                    new SelectListItem{Text=SD.RoleCustomer,Value=SD.RoleCustomer},
                };

            ViewBag.RoleList = roleList;
            return View(obj);
        }

        /// <summary>
        /// Logout the user and clear the token.
        /// </summary>
        /// <returns>Redirects to the Home index page.</returns>
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Sign in the user.
        /// </summary>
        /// <param name="model">LoginResponseDto that contains the user's login information.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private async Task SignInUser(LoginResponseDto model)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(model.Token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

            identity.AddClaim(new Claim(ClaimTypes.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(ClaimTypes.Role,
                jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

        /// <summary>
        /// Get-method for the PersonalData view.
        /// </summary>
        /// <returns>Returns the PersonalData view with a new EditAccountDto object.</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> PersonalData()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                TempData["error"] = "User not found.";
                return RedirectToAction("Index", "Home");
            }

            var response = await _authService.GetUserDetailsAsync(userId);

            if (response == null || !response.IsSuccess)
            {
                TempData["error"] = "Unable to load user details.";
                return RedirectToAction("Index", "Home");
            }

            var user = JsonConvert.DeserializeObject<EditAccountDto>(Convert.ToString(response.Result));
            return View(user);
        }

        /// <summary>
        /// Get-method for editing the user's account details.
        /// </summary>
        /// <returns>Returns the EditAccount view with a new EditAccountDto object.</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditAccount()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                TempData["error"] = "User not found.";
                return RedirectToAction("Index", "Home");
            }

            var response = await _authService.GetUserDetailsAsync(userId);

            if (response == null || !response.IsSuccess)
            {
                TempData["error"] = "Unable to load user details.";
                return RedirectToAction("Index", "Home");
            }

            var user = JsonConvert.DeserializeObject<EditAccountDto>(Convert.ToString(response.Result));
            return View(user);
        }

        /// <summary>
        /// Post-method for editing the user's account details.
        /// </summary>
        /// <param name="model">EditAccountDto that contains the user's updated account information.</param>
        /// <returns>Returns the view with a new EditAccountDto object.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditAccount(EditAccountDto model)
        {
            if (string.IsNullOrEmpty(model.NewName))
            {
                ModelState.AddModelError(nameof(model.NewName), "Name is required.");
            }
            if (string.IsNullOrEmpty(model.NewEmail))
            {
                ModelState.AddModelError(nameof(model.NewEmail), "Email is required.");
            }
            if (string.IsNullOrEmpty(model.NewPhoneNumber))
            {
                ModelState.AddModelError(nameof(model.NewPhoneNumber), "Phone number is required.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var response = await _authService.EditAccountAsync(model);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Account updated successfully.";
                return RedirectToAction(nameof(PersonalData));
            }

            TempData["error"] = response?.Message ?? "Failed to update account.";
            return View(model);
        }

        /// <summary>
        /// Get-method for changing the user's password.
        /// </summary>
        /// <returns>Returns the ChangePassword view with a new ChangePasswordDto object.</returns>
        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View(new ChangePasswordDto());
        }

        /// <summary>
        /// Post-method for changing the user's password.
        /// </summary>
        /// <param name="model">ChangePasswordDto that contains the user's current and new password.</param>
        /// <returns>Returns the ChangePassword view with a new ChangePasswordDto object.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        {
            // Manual validation using ModelState
            if (string.IsNullOrWhiteSpace(model.CurrentPassword))
            {
                ModelState.AddModelError(nameof(model.CurrentPassword), "Current password is required.");
            }
            if (string.IsNullOrWhiteSpace(model.NewPassword))
            {
                ModelState.AddModelError(nameof(model.NewPassword), "New password is required.");
            }
            if (string.IsNullOrWhiteSpace(model.ConfirmNewPassword))
            {
                ModelState.AddModelError(nameof(model.ConfirmNewPassword), "Please confirm the new password.");
            }
            if (!string.IsNullOrWhiteSpace(model.NewPassword) &&
                !model.NewPassword.Equals(model.ConfirmNewPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError(nameof(model.ConfirmNewPassword), "The new password and confirmation do not match.");
            }

            // If ModelState is invalid, return the view with error messages
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // If validation passes, call the service
            var response = await _authService.ChangePasswordAsync(model);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Password changed successfully.";
                return RedirectToAction(nameof(PersonalData));
            }

            TempData["error"] = response?.Message ?? "Failed to change password.";
            return View(model);
        }

        /// <summary>
        /// Get-method for deleting the user's account.
        /// </summary>
        /// <returns>Returns the DeleteAccountConfirmation view with a new EditAccountDto object.</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteAccountConfirmation()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                TempData["error"] = "User not found.";
                return RedirectToAction("Index", "Home");
            }

            var response = await _authService.GetUserDetailsAsync(userId);

            if (response == null || !response.IsSuccess)
            {
                TempData["error"] = "Unable to load user details.";
                return RedirectToAction("Index", "Home");
            }

            var user = JsonConvert.DeserializeObject<EditAccountDto>(Convert.ToString(response.Result));

            return View(user);
        }

        /// <summary>
        /// Post-method for deleting the user's account.
        /// </summary>
        /// <returns>Returns the Index view with a success message if the account was deleted successfully.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteAccount()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                TempData["error"] = "User not found.";
                return RedirectToAction("Index", "Home");
            }

            var response = await _authService.DeleteAccountAsync(userId);

            if (response != null && response.IsSuccess)
            {
                await Logout();
                TempData["success"] = "Account deleted successfully.";
                return RedirectToAction("Index", "Home");
            }

            TempData["error"] = response?.Message ?? "Failed to delete account.";
            return RedirectToAction(nameof(PersonalData));
        }
    }
}
