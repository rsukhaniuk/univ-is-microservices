using AutoMapper;
using SmartMenu.Services.ProductAPI.Data;
using SmartMenu.Services.ProductAPI.Models;
using SmartMenu.Services.ProductAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SmartMenu.Services.ProductAPI.Controllers
{
    /// <summary>
    /// API controller for managing categories.
    /// </summary>
    [Route("api/category")]
    [ApiController]
    public class CategoryAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryAPIController"/> class.
        /// </summary>
        /// <param name="db">The database context.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public CategoryAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        /// <summary>
        /// Gets the list of categories.
        /// </summary>
        /// <returns>The response containing the list of categories.</returns>
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Category> objList = _db.Categories.ToList();
                _response.Result = _mapper.Map<IEnumerable<CategoryDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        /// <summary>
        /// Gets a category by ID.
        /// </summary>
        /// <param name="id">The category ID.</param>
        /// <returns>The response containing the category.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Category obj = _db.Categories.First(u => u.CategoryId == id);
                _response.Result = _mapper.Map<CategoryDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="CategoryDto">The category data transfer object.</param>
        /// <returns>The response containing the created category.</returns>
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Post(CategoryDto CategoryDto)
        {
            try
            {
                Category category = _mapper.Map<Category>(CategoryDto);
                _db.Categories.Add(category);
                _db.SaveChanges();

                _response.Result = _mapper.Map<CategoryDto>(category);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="CategoryDto">The category data transfer object.</param>
        /// <returns>The response containing the updated category.</returns>
        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put(CategoryDto CategoryDto)
        {
            try
            {
                Category category = _mapper.Map<Category>(CategoryDto);

                _db.Categories.Update(category);
                _db.SaveChanges();

                _response.Result = _mapper.Map<CategoryDto>(category);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        /// <summary>
        /// Deletes a category by ID.
        /// </summary>
        /// <param name="id">The category ID.</param>
        /// <returns>The response indicating the result of the delete operation.</returns>
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Category obj = _db.Categories.First(u => u.CategoryId == id);

                _db.Categories.Remove(obj);
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
