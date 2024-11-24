using SmartMenu.Web.Models;
using SmartMenu.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartMenu.Web.Service;

namespace SmartMenu.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }


        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto>? list = new();

            ResponseDto? response = await _productService.GetAllProductsAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }

        public async Task<IActionResult> ProductCreate()
        {
            var categoriesResponse = await _categoryService.GetAllCategoriesAsync(); // Assuming _categoryService exists
            if (categoriesResponse != null && categoriesResponse.IsSuccess)
            {
                var categories = JsonConvert.DeserializeObject<List<CategoryDto>>(categoriesResponse.Result.ToString());
                ViewBag.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.Name
                }).ToList();
            }
            else
            {
                ViewBag.Categories = new List<SelectListItem>();
                TempData["error"] = "Unable to load categories.";
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _productService.CreateProductsAsync(model);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Product created successfully";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            var categoriesResponse = await _categoryService.GetAllCategoriesAsync();
            if (categoriesResponse != null && categoriesResponse.IsSuccess)
            {
                var categories = JsonConvert.DeserializeObject<List<CategoryDto>>(categoriesResponse.Result.ToString());
                ViewBag.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.Name
                }).ToList();
            }
            return View(model);

            //return View(model);
        }

        public async Task<IActionResult> ProductDelete(int productId)
        {
            ResponseDto? response = await _productService.GetProductByIdAsync(productId);

            if (response != null && response.IsSuccess)
            {
                ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductDto productDto)
        {
            ResponseDto? response = await _productService.DeleteProductsAsync(productDto.ProductId);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Product deleted successfully";
                return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(productDto);
        }

        public async Task<IActionResult> ProductEdit(int productId)
        {
            ResponseDto? response = await _productService.GetProductByIdAsync(productId);

            if (response != null && response.IsSuccess)
            {
                ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));

                var categoriesResponse = await _categoryService.GetAllCategoriesAsync();
                if (categoriesResponse != null && categoriesResponse.IsSuccess)
                {
                    var categories = JsonConvert.DeserializeObject<List<CategoryDto>>(categoriesResponse.Result.ToString());
                    ViewBag.Categories = categories.Select(c => new SelectListItem
                    {
                        Value = c.CategoryId.ToString(),
                        Text = c.Name
                    }).ToList();
                }
                else
                {
                    ViewBag.Categories = new List<SelectListItem>();
                }

                return View(model);
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductEdit(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _productService.UpdateProductsAsync(productDto);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Product updated successfully";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }

            var categoriesResponse = await _categoryService.GetAllCategoriesAsync();
            if (categoriesResponse != null && categoriesResponse.IsSuccess)
            {
                var categories = JsonConvert.DeserializeObject<List<CategoryDto>>(categoriesResponse.Result.ToString());
                ViewBag.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.Name
                }).ToList();
            }


            return View(productDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string status)
        {
			List<ProductDto>? list = new();

			ResponseDto? response = await _productService.GetAllProductsAsync();

			if (response != null && response.IsSuccess)
			{
				list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
			}
			else
			{
				list = new List<ProductDto>();
			}

			return Json(new { data = list.OrderByDescending(p => p.ProductId) });

		}
    }
}
