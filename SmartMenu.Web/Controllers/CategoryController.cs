using SmartMenu.Web.Models;
using SmartMenu.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartMenu.Web.Controllers
{
    /// <summary>
    /// Controller for Category in the SmartMenu web application
    /// </summary>
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        /// <summary>
        /// Constructor for the CategoryController
        /// </summary>
        /// <param name="categoryService">service for category</param>
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Method to display the index page for categories
        /// </summary>
        /// <returns>returns the view</returns>
        public async Task<IActionResult> CategoryIndex()
        {
            List<CategoryDto>? list = new();

            ResponseDto? response = await _categoryService.GetAllCategoriesAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CategoryDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }

        /// <summary>
        /// Method to display the create page for categories
        /// </summary>
        /// <returns>returns the view</returns>
        public IActionResult CategoryCreate()
        {
            return View();
        }

        /// <summary>
        /// Post-method to create a category
        /// </summary>
        /// <param name="model">dto for category</param>
        /// <returns>returns the view</returns>
        [HttpPost]
        public async Task<IActionResult> CategoryCreate(CategoryDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _categoryService.CreateCategoryAsync(model);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Category created successfully";
                    return RedirectToAction(nameof(CategoryIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(model);
        }

        /// <summary>
        /// Method to delete a category
        /// </summary>
        /// <param name="categoryId">id of the category</param>
        /// <returns>returns the view</returns>
        public async Task<IActionResult> CategoryDelete(int categoryId)
        {
            ResponseDto? response = await _categoryService.GetCategoryByIdAsync(categoryId);

            if (response != null && response.IsSuccess)
            {
                CategoryDto? model = JsonConvert.DeserializeObject<CategoryDto>(Convert.ToString(response.Result));
                return View(model);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }

        /// <summary>
        /// Post-method to delete a category
        /// </summary>
        /// <param name="categoryDto">dto for category</param>
        /// <returns>returns the view</returns>
        [HttpPost]
        public async Task<IActionResult> CategoryDelete(CategoryDto categoryDto)
        {
            ResponseDto? response = await _categoryService.DeleteCategoryAsync(categoryDto.CategoryId);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Category deleted successfully";
                return RedirectToAction(nameof(CategoryIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(categoryDto);
        }

        public async Task<IActionResult> CategoryEdit(int categoryId)
        {
            ResponseDto? response = await _categoryService.GetCategoryByIdAsync(categoryId);

            if (response != null && response.IsSuccess)
            {
                CategoryDto? model = JsonConvert.DeserializeObject<CategoryDto>(Convert.ToString(response.Result));
                return View(model);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CategoryEdit(CategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _categoryService.UpdateCategoryAsync(categoryDto);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Category updated successfully";
                    return RedirectToAction(nameof(CategoryIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(categoryDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<CategoryDto>? list = new();

            ResponseDto? response = await _categoryService.GetAllCategoriesAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CategoryDto>>(Convert.ToString(response.Result));
            }
            else
            {
                list = new List<CategoryDto>();
            }

            return Json(new { data = list.OrderByDescending(c => c.CategoryId) });
        }
    }
}
