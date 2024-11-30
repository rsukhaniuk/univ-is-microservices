using SmartMenu.Web.Models;
using SmartMenu.Web.Service;
using SmartMenu.Web.Service.IService;
using SmartMenu.Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace SmartMenu.Web.Controllers
{
    /// <summary>
    /// Controller for handling cart operations.
    /// </summary>
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly IAuthService _authService;

        /// <summary>
        /// Constructor for CartController.
        /// </summary>
        /// <param name="cartService">Service for cart operations.</param>
        /// <param name="orderService">Service for order operations.</param>
        /// <param name="authService">Service for authentication operations.</param>
        public CartController(ICartService cartService, IOrderService orderService, IAuthService authService)
        {
            _cartService = cartService;
            _orderService = orderService;
            _authService = authService;
        }

        /// <summary>
        /// Method to display the cart index.
        /// </summary>
        /// <returns>Returns the view for cart index.</returns>
        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartDtoBasedOnLoggedInUser());
        }

        /// <summary>
        /// Method to display the checkout page.
        /// </summary>
        /// <returns>Returns the view for checkout.</returns>
        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            // Load cart based on logged-in user
            var cart = await LoadCartDtoBasedOnLoggedInUser();

            // Fetch user details from AuthService if not already populated
            if (string.IsNullOrEmpty(cart.CartHeader.Name) || string.IsNullOrEmpty(cart.CartHeader.Email) || string.IsNullOrEmpty(cart.CartHeader.Phone))
            {
                var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;

                if (!string.IsNullOrEmpty(userId))
                {
                    ResponseDto? response = await _authService.GetUserDetailsAsync(userId);

                    if (response != null && response.IsSuccess)
                    {
                        var userDetails = JsonConvert.DeserializeObject<EditAccountDto>(response.Result.ToString());
                        if (userDetails != null)
                        {
                            cart.CartHeader.Name = userDetails.NewName;
                            cart.CartHeader.Email = userDetails.NewEmail;
                            cart.CartHeader.Phone = userDetails.NewPhoneNumber;
                        }
                    }
                }
            }

            return View(cart);
        }

        /// <summary>
        /// Post-method to checkout the cart.
        /// </summary>
        /// <param name="cartDto">DTO for the cart.</param>
        /// <returns>Returns the view for cart index.</returns>
        [HttpPost]
        [ActionName("Checkout")]
        public async Task<IActionResult> Checkout(CartDto cartDto)
        {
            CartDto cart = await LoadCartDtoBasedOnLoggedInUser();

            cart.CartHeader.Phone = cartDto.CartHeader.Phone;
            cart.CartHeader.Email = cartDto.CartHeader.Email;
            cart.CartHeader.Name = cartDto.CartHeader.Name;

            var response = await _orderService.CreateOrder(cart);
            OrderHeaderDto orderHeaderDto = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(response.Result));

            if (response != null && response.IsSuccess)
            {
                // Get Stripe session and redirect to Stripe to place order
                var domain = Request.Scheme + "://" + Request.Host.Value + "/";

                StripeRequestDto stripeRequestDto = new()
                {
                    ApprovedUrl = domain + "cart/Confirmation?orderId=" + orderHeaderDto.OrderHeaderId,
                    CancelUrl = domain + "cart/checkout",
                    OrderHeader = orderHeaderDto
                };

                var stripeResponse = await _orderService.CreateStripeSession(stripeRequestDto);
                StripeRequestDto stripeResponseResult = JsonConvert.DeserializeObject<StripeRequestDto>(Convert.ToString(stripeResponse.Result));
                Response.Headers.Add("Location", stripeResponseResult.StripeSessionUrl);

                var clearCartResponse = await _cartService.ClearCartAsync(cart.CartHeader.UserId);
                if (clearCartResponse == null || !clearCartResponse.IsSuccess)
                {
                    TempData["error"] = "Failed to clear cart after order placement.";
                    return View(cart);
                }

                return new StatusCodeResult(303);
            }
            return View();
        }

        /// <summary>
        /// Method to confirm the order.
        /// </summary>
        /// <param name="orderId">ID of the order.</param>
        /// <returns>Returns the view for confirmation.</returns>
        public async Task<IActionResult> Confirmation(int orderId)
        {
            ResponseDto? response = await _orderService.ValidateStripeSession(orderId);
            if (response != null & response.IsSuccess)
            {
                OrderHeaderDto orderHeader = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(response.Result));
                if (orderHeader.Status == SD.Status_Approved)
                {
                    return View(orderId);
                }
            }
            // Redirect to some error page based on status
            return View(orderId);
        }

        /// <summary>
        /// Method to remove a product from the cart.
        /// </summary>
        /// <param name="cartDetailsId">ID of the cart details.</param>
        /// <returns>Returns the view for cart index.</returns>
        public async Task<IActionResult> Remove(int cartDetailsId)
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDto? response = await _cartService.RemoveFromCartAsync(cartDetailsId);
            if (response != null & response.IsSuccess)
            {
                TempData["success"] = "Cart updated successfully";
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }

        /// <summary>
        /// Post-method to apply a coupon.
        /// </summary>
        /// <param name="cartDto">DTO for the cart.</param>
        /// <returns>Returns the view for cart index.</returns>
        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(CartDto cartDto)
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDto? response1 = await _cartService.GetCartByUserIdAsnyc(userId);
            CartDto cartDto1 = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response1.Result));

            cartDto1.CartHeader.CouponCode = cartDto.CartHeader.CouponCode;

            ResponseDto? response = await _cartService.ApplyCouponAsync(cartDto1);

            if (response != null)
            {
                if (response.IsSuccess)
                {
                    TempData["success"] = "Coupon applied successfully.";
                }
                else
                {
                    TempData["error"] = response.Message ?? "Failed to apply coupon.";
                }
            }
            else
            {
                TempData["error"] = "An error occurred while applying the coupon.";
            }

            return RedirectToAction(nameof(CartIndex));
        }

        /// <summary>
        /// Post-method to remove a coupon.
        /// </summary>
        /// <param name="cartDto">DTO for the cart.</param>
        /// <returns>Returns the view for cart index.</returns>
        [HttpPost]
        public async Task<IActionResult> RemoveCoupon(CartDto cartDto)
        {
            cartDto.CartHeader.CouponCode = "";
            ResponseDto? response = await _cartService.ApplyCouponAsync(cartDto);
            if (response != null & response.IsSuccess)
            {
                TempData["success"] = "Cart updated successfully";
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }

        /// <summary>
        /// Method to increase the quantity of a product in the cart.
        /// </summary>
        /// <param name="cartDetailsId">ID of the cart details.</param>
        /// <returns>Returns the view for cart index.</returns>
        public async Task<IActionResult> IncreaseQuantity(int cartDetailsId)
        {
            var response = await _cartService.IncreaseQuantityAsync(cartDetailsId);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Quantity increased successfully!";
            }
            else
            {
                TempData["error"] = response?.Message ?? "Failed to increase quantity!";
            }

            return RedirectToAction(nameof(CartIndex));
        }

        /// <summary>
        /// Method to decrease the quantity of a product in the cart.
        /// </summary>
        /// <param name="cartDetailsId">ID of the cart details.</param>
        /// <returns>Returns the view for cart index.</returns>
        public async Task<IActionResult> DecreaseQuantity(int cartDetailsId)
        {
            var response = await _cartService.DecreaseQuantityAsync(cartDetailsId);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Quantity decreased successfully!";
            }
            else
            {
                TempData["error"] = response?.Message ?? "Failed to decrease quantity!";
            }

            return RedirectToAction(nameof(CartIndex));
        }

        /// <summary>
        /// Method to load the cart DTO based on the logged-in user.
        /// </summary>
        /// <returns>Returns the cart DTO.</returns>
        private async Task<CartDto> LoadCartDtoBasedOnLoggedInUser()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDto? response = await _cartService.GetCartByUserIdAsnyc(userId);
            if (response != null & response.IsSuccess)
            {
                CartDto cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response.Result));
                return cartDto;
            }
            return new CartDto();
        }
    }
}
