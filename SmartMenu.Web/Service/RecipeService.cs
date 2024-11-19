using SmartMenu.Web.Models;
using SmartMenu.Web.Service.IService;
using SmartMenu.Web.Utility;

namespace SmartMenu.Web.Service
{
    public class RecipeService : IRecipeService
    {
        private readonly IBaseService _baseService;
        public RecipeService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateAsync(RecipeDto recipeDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = recipeDto,
                Url = SD.RecipeAPIBase + "/api/recipe",
                ContentType = SD.ContentType.MultipartFormData
            });
        }

        public async Task<ResponseDto?> DeleteAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.RecipeAPIBase + "/api/recipe/" + id
            });
        }

        public async Task<ResponseDto?> GetAllAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.RecipeAPIBase + "/api/recipe"
            });
        }



        public async Task<ResponseDto?> GetByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.RecipeAPIBase + "/api/recipe/" + id
            });
        }

        public async Task<ResponseDto?> UpdateAsync(RecipeDto recipeDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = recipeDto,
                Url = SD.RecipeAPIBase + "/api/recipe",
                ContentType = SD.ContentType.MultipartFormData
            });
        }
    }
}
