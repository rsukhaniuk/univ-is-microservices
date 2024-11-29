using IdentityModel;
using SmartMenu.Web.Models;
using SmartMenu.Web.Service.IService;
using SmartMenu.Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace SmartMenu.Web.Controllers
{
    /// <summary>
    /// Controller for managing the home page and related actions in the SmartMenu web application.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ICategoryService _categoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="productService">The service for managing products.</param>
        /// <param name="cartService">The service for managing the shopping cart.</param>
        /// <param name="categoryService">The service for managing categories.</param>
        public HomeController(IProductService productService, ICartService cartService, ICategoryService categoryService)
        {
            _productService = productService;
            _cartService = cartService;
            _categoryService = categoryService;
        }

        /// <summary>
        /// Displays the index page with a list of products.
        /// </summary>
        /// <param name="searchTerm">The search term to filter products.</param>
        /// <param name="categoryId">The category ID to filter products.</param>
        /// <param name="pageNumber">The current page number for pagination.</param>
        /// <param name="sortOrder">The sort order for products.</param>
        /// <returns>Returns the view with the list of products.</returns>
        public async Task<IActionResult> Index(string? searchTerm, int? categoryId, int pageNumber = 1, string? sortOrder = null)
        {
            const int pageSize = 6;
            List<ProductDto>? list = new();
            List<CategoryDto>? categories = new();

            // Fetch all categories
            ResponseDto? categoryResponse = await _categoryService.GetAllCategoriesAsync();
            if (categoryResponse != null && categoryResponse.IsSuccess)
            {
                categories = JsonConvert.DeserializeObject<List<CategoryDto>>(Convert.ToString(categoryResponse.Result));
                ViewBag.Categories = categories;
            }
            else
            {
                TempData["error"] = categoryResponse?.Message;
            }

            // Fetch all products
            ResponseDto? response = await _productService.GetAllProductsAsync();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));

                // Filter by search term
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    list = list.Where(p =>
                        p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                        p.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                // Filter by category
                if (categoryId.HasValue)
                {
                    list = list.Where(p => p.CategoryId == categoryId.Value).ToList();
                }

                // Sort products
                list = sortOrder switch
                {
                    "priceAsc" => list.OrderBy(p => p.Price).ToList(),
                    "priceDesc" => list.OrderByDescending(p => p.Price).ToList(),
                    "nameAsc" => list.OrderBy(p => p.Name).ToList(),
                    "nameDesc" => list.OrderByDescending(p => p.Name).ToList(),
                    _ => list.OrderBy(p => p.ProductId).ToList(), // Default sort by ProductId
                };
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            // Pagination
            int totalItems = list.Count;
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var paginatedList = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            // Pass pagination details to the view
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;

            return View(paginatedList);
        }

        /// <summary>
        /// Displays the details of a product.
        /// </summary>
        /// <param name="productId">The ID of the product to display.</param>
        /// <returns>Returns the view with the product details.</returns>
        public async Task<IActionResult> ProductDetails(int productId)
        {
            ProductDto? model = new();

            ResponseDto? response = await _productService.GetProductByIdAsync(productId);

            if (response != null && response.IsSuccess)
            {
                model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(model);
        }

        /// <summary>
        /// Handles the post request to add a product to the shopping cart.
        /// </summary>
        /// <param name="productDto">The data transfer object for the product to add to the cart.</param>
        /// <returns>Returns the view with the product details or redirects to the index page.</returns>
        [Authorize]
        [HttpPost]
        [ActionName("ProductDetails")]
        public async Task<IActionResult> ProductDetails(ProductDto productDto)
        {
            CartDto cartDto = new CartDto()
            {
                CartHeader = new CartHeaderDto
                {
                    UserId = User.Claims.Where(u => u.Type == JwtClaimTypes.Subject)?.FirstOrDefault()?.Value
                }
            };

            CartDetailsDto cartDetails = new CartDetailsDto()
            {
                Count = productDto.Count,
                ProductId = productDto.ProductId,
            };

            List<CartDetailsDto> cartDetailsDtos = new() { cartDetails };
            cartDto.CartDetails = cartDetailsDtos;

            ResponseDto? response = await _cartService.UpsertCartAsync(cartDto);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Item has been added to the Shopping Cart";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(productDto);
        }

        /// <summary>
        /// Displays the privacy policy page.
        /// </summary>
        /// <returns>Returns the view for the privacy policy.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Displays the error page.
        /// </summary>
        /// <returns>Returns the view for the error page.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}