using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartMenu.Web.Models;
using SmartMenu.Web.Service.IService;

namespace SmartMenu.Web.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeService _recipeService;
        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }


        public async Task<IActionResult> RecipeIndex()
        {
            List<RecipeDto>? list = new();

            ResponseDto? response = await _recipeService.GetAllAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<RecipeDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }

        public async Task<IActionResult> RecipeCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RecipeCreate(RecipeDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _recipeService.CreateAsync(model);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Recipe created successfully";
                    return RedirectToAction(nameof(RecipeIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(model);
        }

        public async Task<IActionResult> RecipeDelete(int recipeId)
        {
            ResponseDto? response = await _recipeService.GetByIdAsync(recipeId);

            if (response != null && response.IsSuccess)
            {
                RecipeDto? model = JsonConvert.DeserializeObject<RecipeDto>(Convert.ToString(response.Result));
                return View(model);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> RecipeDelete(RecipeDto recipeDto)
        {
            ResponseDto? response = await _recipeService.DeleteAsync(recipeDto.RecipeId);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Recipe deleted successfully";
                return RedirectToAction(nameof(RecipeIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(recipeDto);
        }

        public async Task<IActionResult> RecipeEdit(int recipeId)
        {
            ResponseDto? response = await _recipeService.GetByIdAsync(recipeId);

            if (response != null && response.IsSuccess)
            {
                RecipeDto? model = JsonConvert.DeserializeObject<RecipeDto>(Convert.ToString(response.Result));
                return View(model);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> RecipeEdit(RecipeDto recipeDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _recipeService.UpdateAsync(recipeDto);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Recipe updated successfully";
                    return RedirectToAction(nameof(RecipeIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(recipeDto);
        }

    }
}
