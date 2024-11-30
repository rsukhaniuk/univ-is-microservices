using SmartMenu.Web.Models;
using SmartMenu.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartMenu.Web.Controllers
{
    /// <summary>
    /// Controller for Category in the SmartMenu web application.
    /// </summary>
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        /// <summary>
        /// Constructor for the CategoryController.
        /// </summary>
        /// <param name="categoryService">Service for category operations.</param>
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Method to display the index page for categories.
        /// </summary>
        /// <returns>Returns the view for category index.</returns>
        public async Task<IActionResult> CategoryIndex()
        {
            // Initialize an empty list of CategoryDto
            List<CategoryDto>? list = new();

            // Call the service to get all categories
            ResponseDto? response = await _categoryService.GetAllCategoriesAsync();

            // If the response is successful, deserialize the result into a list of CategoryDto
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CategoryDto>>(Convert.ToString(response.Result));
            }
            else
            {
                // If there is an error, store the error message in TempData
                TempData["error"] = response?.Message;
            }

            // Return the view with the list of categories
            return View(list);
        }

        /// <summary>
        /// Method to display the create page for categories.
        /// </summary>
        /// <returns>Returns the view for creating a category.</returns>
        public IActionResult CategoryCreate()
        {
            // Return the view for creating a category
            return View();
        }

        /// <summary>
        /// Post-method to create a category.
        /// </summary>
        /// <param name="model">DTO for category.</param>
        /// <returns>Returns the view for category index.</returns>
        [HttpPost]
        public async Task<IActionResult> CategoryCreate(CategoryDto model)
        {
            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                // Call the service to create a new category
                ResponseDto? response = await _categoryService.CreateCategoryAsync(model);

                // If the response is successful, store a success message in TempData and redirect to the index page
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Category created successfully";
                    return RedirectToAction(nameof(CategoryIndex));
                }
                else
                {
                    // If there is an error, store the error message in TempData
                    TempData["error"] = response?.Message;
                }
            }
            // Return the view with the model if the model state is not valid or there was an error
            return View(model);
        }

        /// <summary>
        /// Method to display the delete page for a category.
        /// </summary>
        /// <param name="categoryId">ID of the category.</param>
        /// <returns>Returns the view for deleting a category.</returns>
        public async Task<IActionResult> CategoryDelete(int categoryId)
        {
            // Call the service to get the category by ID
            ResponseDto? response = await _categoryService.GetCategoryByIdAsync(categoryId);

            // If the response is successful, deserialize the result into a CategoryDto and return the view
            if (response != null && response.IsSuccess)
            {
                CategoryDto? model = JsonConvert.DeserializeObject<CategoryDto>(Convert.ToString(response.Result));
                return View(model);
            }
            else
            {
                // If there is an error, store the error message in TempData
                TempData["error"] = response?.Message;
            }
            // Return a NotFound result if the category was not found
            return NotFound();
        }

        /// <summary>
        /// Post-method to delete a category.
        /// </summary>
        /// <param name="categoryDto">DTO for category.</param>
        /// <returns>Returns the view for category index.</returns>
        [HttpPost]
        public async Task<IActionResult> CategoryDelete(CategoryDto categoryDto)
        {
            // Call the service to delete the category
            ResponseDto? response = await _categoryService.DeleteCategoryAsync(categoryDto.CategoryId);

            // If the response is successful, store a success message in TempData and redirect to the index page
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Category deleted successfully";
                return RedirectToAction(nameof(CategoryIndex));
            }
            else
            {
                // If there is an error, store the error message in TempData
                TempData["error"] = response?.Message;
            }
            // Return the view with the model if there was an error
            return View(categoryDto);
        }

        /// <summary>
        /// Method to display the edit page for a category.
        /// </summary>
        /// <param name="categoryId">ID of the category.</param>
        /// <returns>Returns the view for editing a category.</returns>
        public async Task<IActionResult> CategoryEdit(int categoryId)
        {
            // Call the service to get the category by ID
            ResponseDto? response = await _categoryService.GetCategoryByIdAsync(categoryId);

            // If the response is successful, deserialize the result into a CategoryDto and return the view
            if (response != null && response.IsSuccess)
            {
                CategoryDto? model = JsonConvert.DeserializeObject<CategoryDto>(Convert.ToString(response.Result));
                return View(model);
            }
            else
            {
                // If there is an error, store the error message in TempData
                TempData["error"] = response?.Message;
            }
            // Return a NotFound result if the category was not found
            return NotFound();
        }

        /// <summary>
        /// Post-method to edit a category.
        /// </summary>
        /// <param name="categoryDto">DTO for category.</param>
        /// <returns>Returns the view for category index.</returns>
        [HttpPost]
        public async Task<IActionResult> CategoryEdit(CategoryDto categoryDto)
        {
            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                // Call the service to update the category
                ResponseDto? response = await _categoryService.UpdateCategoryAsync(categoryDto);

                // If the response is successful, store a success message in TempData and redirect to the index page
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Category updated successfully";
                    return RedirectToAction(nameof(CategoryIndex));
                }
                else
                {
                    // If there is an error, store the error message in TempData
                    TempData["error"] = response?.Message;
                }
            }
            // Return the view with the model if the model state is not valid or there was an error
            return View(categoryDto);
        }

        /// <summary>
        /// Method to get all categories.
        /// </summary>
        /// <returns>Returns a JSON result with the list of categories.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Initialize an empty list of CategoryDto
            List<CategoryDto>? list = new();

            // Call the service to get all categories
            ResponseDto? response = await _categoryService.GetAllCategoriesAsync();

            // If the response is successful, deserialize the result into a list of CategoryDto
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CategoryDto>>(Convert.ToString(response.Result));
            }
            else
            {
                // If there is an error, initialize an empty list
                list = new List<CategoryDto>();
            }

            // Return a JSON result with the list of categories ordered by CategoryId in descending order
            return Json(new { data = list.OrderByDescending(c => c.CategoryId) });
        }
    }
}
