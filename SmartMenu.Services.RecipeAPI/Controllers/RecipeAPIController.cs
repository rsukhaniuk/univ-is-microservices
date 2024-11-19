using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartMenu.Services.RecipeAPI.Data;
using SmartMenu.Services.RecipeAPI.Models;
using SmartMenu.Services.RecipeAPI.Models.Dto;

namespace SmartMenu.Services.RecipeAPI.Controllers
{
    [Route("api/recipe")]
    [ApiController]
    public class RecipeAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;

        public RecipeAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Recipe> objList = _db.Recipes.ToList();
                _response.Result = _mapper.Map<IEnumerable<RecipeDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Recipe obj = _db.Recipes.First(u => u.RecipeId == id);
                _response.Result = _mapper.Map<RecipeDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Post(RecipeDto RecipeDto)
        {
            try
            {
                Recipe recipe = _mapper.Map<Recipe>(RecipeDto);
                _db.Recipes.Add(recipe);
                _db.SaveChanges();

                if (RecipeDto.Image != null)
                {

                    string fileName = recipe.RecipeId + Path.GetExtension(RecipeDto.Image.FileName);
                    string filePath = @"wwwroot\RecipeImages\" + fileName;

                    //I have added the if condition to remove the any image with same name if that exist in the folder by any change
                    var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    FileInfo file = new FileInfo(directoryLocation);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                    {
                        RecipeDto.Image.CopyTo(fileStream);
                    }
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    recipe.ImageUrl = baseUrl + "/RecipeImages/" + fileName;
                    recipe.ImageLocalPath = filePath;
                }
                else
                {
                    recipe.ImageUrl = "https://placehold.co/600x400";
                }
                _db.Recipes.Update(recipe);
                _db.SaveChanges();
                _response.Result = _mapper.Map<RecipeDto>(recipe);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put(RecipeDto RecipeDto)
        {
            try
            {
                Recipe recipe = _mapper.Map<Recipe>(RecipeDto);

                if (RecipeDto.Image != null)
                {
                    if (!string.IsNullOrEmpty(recipe.ImageLocalPath))
                    {
                        var oldFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), recipe.ImageLocalPath);
                        FileInfo file = new FileInfo(oldFilePathDirectory);
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }

                    string fileName = recipe.RecipeId + Path.GetExtension(RecipeDto.Image.FileName);
                    string filePath = @"wwwroot\RecipeImages\" + fileName;
                    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                    {
                        RecipeDto.Image.CopyTo(fileStream);
                    }
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    recipe.ImageUrl = baseUrl + "/RecipeImages/" + fileName;
                    recipe.ImageLocalPath = filePath;
                }


                _db.Recipes.Update(recipe);
                _db.SaveChanges();

                _response.Result = _mapper.Map<RecipeDto>(recipe);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Recipe obj = _db.Recipes.First(u => u.RecipeId == id);
                if (!string.IsNullOrEmpty(obj.ImageLocalPath))
                {
                    var oldFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), obj.ImageLocalPath);
                    FileInfo file = new FileInfo(oldFilePathDirectory);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
                _db.Recipes.Remove(obj);
                _db.SaveChanges();
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
