using AutoMapper;
using SmartMenu.Services.ShoppingCartAPI.Data;
using SmartMenu.Services.ShoppingCartAPI.Models;
using SmartMenu.Services.ShoppingCartAPI.Models.Dto;
using SmartMenu.Services.ShoppingCartAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

namespace SmartMenu.Services.ShoppingCartAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartAPIController : ControllerBase
    {
        private ResponseDto _response;
        private IMapper _mapper;
        private readonly AppDbContext _db;
        private IProductService _productService;
        private ICouponService _couponService;
        private IConfiguration _configuration;
        public CartAPIController(AppDbContext db,
            IMapper mapper, IProductService productService, ICouponService couponService, IConfiguration configuration)
        {
            _db = db;
            _productService = productService;
            this._response = new ResponseDto();
            _mapper = mapper;
            _couponService = couponService;
            _configuration = configuration;
        }
        [HttpGet("GetCart/{userId}")]
        public async Task<ResponseDto> GetCart(string userId)
        {
            try
            {
                CartDto cart = new()
                {
                    CartHeader = _mapper.Map<CartHeaderDto>(_db.CartHeaders.First(u => u.UserId == userId))
                };
                cart.CartDetails = _mapper.Map<IEnumerable<CartDetailsDto>>(_db.CartDetails
                    .Where(u=>u.CartHeaderId==cart.CartHeader.CartHeaderId));

                IEnumerable<ProductDto> productDtos = await _productService.GetProducts();

                foreach (var item in cart.CartDetails)
                {
                    item.Product = productDtos.FirstOrDefault(u => u.ProductId == item.ProductId);
                    cart.CartHeader.CartTotal += (item.Count * item.Product.Price);
                }

                //apply coupon if any
                if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
                {
                    CouponDto coupon = await _couponService.GetCoupon(cart.CartHeader.CouponCode);
                    if(coupon!=null && cart.CartHeader.CartTotal > coupon.MinAmount)
                    {
                        cart.CartHeader.CartTotal -= coupon.DiscountAmount;
                        cart.CartHeader.Discount=coupon.DiscountAmount;
                    }
                }

                _response.Result=cart;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPost("ApplyCoupon")]
        public async Task<object> ApplyCoupon([FromBody] CartDto cartDto)
        {
            try
            {
                // Retrieve the cart header from the database
                var cartFromDb = await _db.CartHeaders.FirstOrDefaultAsync(u => u.UserId == cartDto.CartHeader.UserId);
                if (cartFromDb == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Cart not found.";
                    return _response;
                }

                // If CouponCode is empty, clear the coupon without validation
                if (string.IsNullOrEmpty(cartDto.CartHeader.CouponCode))
                {
                    cartFromDb.CouponCode = cartDto.CartHeader.CouponCode;
                    _db.CartHeaders.Update(cartFromDb);
                    await _db.SaveChangesAsync();
                    _response.Result = true;

                    return _response;
                }

                // Use CouponService to validate the coupon
                var coupon = await _couponService.GetCoupon(cartDto.CartHeader.CouponCode);
                if (coupon == null || string.IsNullOrEmpty(coupon.CouponCode))
                {
                    _response.IsSuccess = false;
                    _response.Message = "Invalid coupon code.";
                    return _response;
                }

                // Validate the cart total against the coupon's minimum amount
                if (cartDto.CartHeader.CartTotal < coupon.MinAmount)
                {
                    _response.IsSuccess = false;
                    _response.Message = $"Cart total must be at least {coupon.MinAmount:c} to use this coupon.";
                    return _response;
                }

                // Apply the coupon
                cartFromDb.CouponCode = cartDto.CartHeader.CouponCode;
                cartFromDb.Discount = coupon.DiscountAmount; // Optionally store the discount amount
                _db.CartHeaders.Update(cartFromDb);
                await _db.SaveChangesAsync();

                _response.IsSuccess = true;
                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.ToString();
            }

            return _response;
        }

        [HttpPost("EmailCartRequest")]
        public async Task<object> EmailCartRequest([FromBody] CartDto cartDto)
        {
            try
            {
                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.ToString();
            }
            return _response;
        }




        [HttpPost("CartUpsert")]
        public async Task<ResponseDto> CartUpsert(CartDto cartDto)
        {
            try
            {
                var cartHeaderFromDb = await _db.CartHeaders.AsNoTracking()
                    .FirstOrDefaultAsync(u => u.UserId == cartDto.CartHeader.UserId);
                if (cartHeaderFromDb == null)
                {
                    //create header and details
                    CartHeader cartHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);
                    _db.CartHeaders.Add(cartHeader);
                    await _db.SaveChangesAsync();
                    cartDto.CartDetails.First().CartHeaderId = cartHeader.CartHeaderId;
                    _db.CartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                    await _db.SaveChangesAsync();
                }
                else
                {
                    //if header is not null
                    //check if details has same product
                    var cartDetailsFromDb = await _db.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                        u => u.ProductId == cartDto.CartDetails.First().ProductId &&
                        u.CartHeaderId == cartHeaderFromDb.CartHeaderId);
                    if (cartDetailsFromDb == null)
                    {
                        //create cartdetails
                        cartDto.CartDetails.First().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                        _db.CartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                        await _db.SaveChangesAsync();
                    }
                    else
                    {
                        //update count in cart details
                        cartDto.CartDetails.First().Count += cartDetailsFromDb.Count;
                        cartDto.CartDetails.First().CartHeaderId = cartDetailsFromDb.CartHeaderId;
                        cartDto.CartDetails.First().CartDetailsId = cartDetailsFromDb.CartDetailsId;
                        _db.CartDetails.Update(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                        await _db.SaveChangesAsync();
                    }
                }
                _response.Result = cartDto;
            }
            catch (Exception ex)
            {
                _response.Message= ex.Message.ToString();
                _response.IsSuccess= false;
            }
            return _response;
        }

        [HttpPost("IncreaseQuantity/{cartDetailsId}")]
        public async Task<ResponseDto> IncreaseQuantity(int cartDetailsId)
        {
            try
            {
                var cartDetails = await _db.CartDetails.Include(cd => cd.CartHeader)
                    .FirstOrDefaultAsync(cd => cd.CartDetailsId == cartDetailsId);

                if (cartDetails == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Cart item not found.";
                    return _response;
                }

                cartDetails.Count += 1; // Increase quantity
                _db.CartDetails.Update(cartDetails);
                await _db.SaveChangesAsync();

                _response.IsSuccess = true;
                _response.Result = _mapper.Map<CartDetailsDto>(cartDetails);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPost("DecreaseQuantity/{cartDetailsId}")]
        public async Task<ResponseDto> DecreaseQuantity(int cartDetailsId)
        {
            try
            {
                var cartDetails = await _db.CartDetails.Include(cd => cd.CartHeader)
                    .FirstOrDefaultAsync(cd => cd.CartDetailsId == cartDetailsId);

                if (cartDetails == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Cart item not found.";
                    return _response;
                }

                if (cartDetails.Count > 1)
                {
                    cartDetails.Count -= 1; // Decrease quantity
                    _db.CartDetails.Update(cartDetails);
                    await _db.SaveChangesAsync();

                    _response.IsSuccess = true;
                    _response.Result = _mapper.Map<CartDetailsDto>(cartDetails);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = "Quantity cannot be less than 1.";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }



        [HttpPost("RemoveCart")]
        public async Task<ResponseDto> RemoveCart([FromBody]int cartDetailsId)
        {
            try
            {
                CartDetails cartDetails = _db.CartDetails
                   .First(u => u.CartDetailsId == cartDetailsId);

                int totalCountofCartItem = _db.CartDetails.Where(u => u.CartHeaderId == cartDetails.CartHeaderId).Count();
                _db.CartDetails.Remove(cartDetails);
                if (totalCountofCartItem == 1)
                {
                    var cartHeaderToRemove = await _db.CartHeaders
                       .FirstOrDefaultAsync(u => u.CartHeaderId == cartDetails.CartHeaderId);

                    _db.CartHeaders.Remove(cartHeaderToRemove);
                }
                await _db.SaveChangesAsync();
               
                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpPost("ClearCart/{userId}")]
        public async Task<ResponseDto> ClearCart(string userId)
        {
            try
            {
                // Retrieve the CartHeader for the user
                var cartHeader = await _db.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId);

                if (cartHeader != null)
                {
                    // Remove all CartDetails associated with the CartHeader
                    var cartDetails = _db.CartDetails.Where(u => u.CartHeaderId == cartHeader.CartHeaderId);
                    _db.CartDetails.RemoveRange(cartDetails);

                    // Remove the CartHeader itself
                    _db.CartHeaders.Remove(cartHeader);

                    await _db.SaveChangesAsync();
                }

                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

    }
}
