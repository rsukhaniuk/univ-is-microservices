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
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;

        public AuthController(IAuthService authService, ITokenProvider  tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider; 
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }

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

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto obj)
        {
            ResponseDto result = await _authService.RegisterAsync(obj);
            ResponseDto assingRole;

            if(result!=null && result.IsSuccess)
            {
                if (string.IsNullOrEmpty(obj.Role))
                {
                    obj.Role = SD.RoleCustomer;
                }
                assingRole = await _authService.AssignRoleAsync(obj);
                if (assingRole!=null && assingRole.IsSuccess)
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


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Index","Home");
        }


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

        // GET: Edit Account
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

        // POST: Edit Account
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
                //// Refresh the user's claims
                //await RefreshAuthenticationClaims();

                TempData["success"] = "Account updated successfully.";
                return RedirectToAction(nameof(PersonalData));
            }

            TempData["error"] = response?.Message ?? "Failed to update account.";
            return View(model);
        }

        //private async Task RefreshAuthenticationClaims()
        //{
        //    var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        //    if (!string.IsNullOrEmpty(userId))
        //    {
        //        var userDetailsResponse = await _authService.GetUserDetailsAsync(userId);

        //        if (userDetailsResponse != null && userDetailsResponse.IsSuccess)
        //        {
        //            var user = JsonConvert.DeserializeObject<EditAccountDto>(Convert.ToString(userDetailsResponse.Result));

        //            // Create a new claims identity
        //            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        //            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserId));
        //            identity.AddClaim(new Claim(ClaimTypes.Name, user.NewName ?? ""));
        //            identity.AddClaim(new Claim(ClaimTypes.Email, user.NewEmail ?? ""));

        //            var principal = new ClaimsPrincipal(identity);

        //            // Refresh the authentication cookie
        //            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        //        }
        //    }
        //}

        // GET: Change Password
        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View(new ChangePasswordDto());
        }

        // POST: Change Password
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        {
            var response = await _authService.ChangePasswordAsync(model);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Password changed successfully.";
                return RedirectToAction(nameof(PersonalData));
            }

            TempData["error"] = response?.Message ?? "Failed to change password.";
            return View(model);
        }

        // POST: Delete Account
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteAccount()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

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
