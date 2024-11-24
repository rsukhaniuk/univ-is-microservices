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
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly IAuthService _authService;
        public CartController(ICartService cartService, IOrderService orderService, IAuthService authService)
        {
            _cartService = cartService;
            _orderService = orderService;
            _authService = authService;
        }

        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartDtoBasedOnLoggedInUser());
        }

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
                //get stripe session and redirect to stripe to place order
                //
                var domain = Request.Scheme + "://" + Request.Host.Value + "/";

                StripeRequestDto stripeRequestDto = new()
                {
                    ApprovedUrl = domain + "cart/Confirmation?orderId=" + orderHeaderDto.OrderHeaderId,
                    CancelUrl = domain + "cart/checkout",
                    OrderHeader = orderHeaderDto
                };

                var stripeResponse = await _orderService.CreateStripeSession(stripeRequestDto);
                StripeRequestDto stripeResponseResult = JsonConvert.DeserializeObject<StripeRequestDto>
                                            (Convert.ToString(stripeResponse.Result));
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
            //redirect to some error page based on status
            return View(orderId);
        }

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
        [HttpPost]
        public async Task<IActionResult> EmailCart(CartDto cartDto)
        {
            CartDto cart = await LoadCartDtoBasedOnLoggedInUser();
            cart.CartHeader.Email = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Email)?.FirstOrDefault()?.Value;
            ResponseDto? response = await _cartService.EmailCart(cart);
            if (response != null & response.IsSuccess)
            {
                TempData["success"] = "Email will be processed and sent shortly.";
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }

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

        [HttpPost]
        public async Task<IActionResult> IncreaseQuantity(int cartDetailsId)
        {
            var cartDto = await GetCartDetailsForUpdate(cartDetailsId);
            if (cartDto != null)
            {
                cartDto.CartDetails.First().Count += 1; // Increase quantity
                var response = await _cartService.UpsertCartAsync(cartDto);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Quantity increased successfully!";
                    return RedirectToAction(nameof(CartIndex));
                }
            }

            TempData["error"] = "Failed to increase quantity!";
            return RedirectToAction(nameof(CartIndex));
        }

        [HttpPost]
        public async Task<IActionResult> DecreaseQuantity(int cartDetailsId)
        {
            var cartDto = await GetCartDetailsForUpdate(cartDetailsId);
            if (cartDto != null)
            {
                if (cartDto.CartDetails.First().Count > 1)
                {
                    cartDto.CartDetails.First().Count -= 1; // Decrease quantity
                    var response = await _cartService.UpsertCartAsync(cartDto);

                    if (response != null && response.IsSuccess)
                    {
                        TempData["success"] = "Quantity decreased successfully!";
                        return RedirectToAction(nameof(CartIndex));
                    }
                }
                else
                {
                    TempData["error"] = "Quantity cannot be less than 1.";
                    return RedirectToAction(nameof(CartIndex));
                }
            }

            TempData["error"] = "Failed to decrease quantity!";
            return RedirectToAction(nameof(CartIndex));
        }

        private async Task<CartDto?> GetCartDetailsForUpdate(int cartDetailsId)
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                TempData["error"] = "User not logged in.";
                return null;
            }

            // Retrieve the current cart
            var response = await _cartService.GetCartByUserIdAsnyc(userId);
            if (response != null && response.IsSuccess)
            {
                var cartDto = JsonConvert.DeserializeObject<CartDto>(response.Result.ToString());
                var cartDetails = cartDto.CartDetails?.FirstOrDefault(cd => cd.CartDetailsId == cartDetailsId);

                if (cartDetails != null)
                {
                    cartDto.CartDetails = new List<CartDetailsDto> { cartDetails };
                    return cartDto;
                }
            }

            return null;
        }


        private async Task<CartDto> LoadCartDtoBasedOnLoggedInUser()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDto? response = await _cartService.GetCartByUserIdAsnyc(userId);
            if(response!=null & response.IsSuccess)
            {
                CartDto cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response.Result));
                return cartDto;
            }


           

            return new CartDto();
        }
    }
}
