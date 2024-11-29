﻿using AutoMapper;
using SmartMenu.Services.ProductAPI.Data;
using SmartMenu.Services.ProductAPI.Models;
using SmartMenu.Services.ProductAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SmartMenu.Services.ProductAPI.Controllers
{
    /// <summary>
    /// API controller for managing products.
    /// </summary>
    [Route("api/product")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductAPIController"/> class.
        /// </summary>
        /// <param name="db">The database context.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public ProductAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        /// <summary>
        /// Gets the list of products.
        /// </summary>
        /// <returns>The response containing the list of products.</returns>
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                var products = _db.Products.Include(p => p.Category).ToList(); // Include Category
                _response.Result = _mapper.Map<IEnumerable<ProductDto>>(products);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        /// <summary>
        /// Gets a product by ID.
        /// </summary>
        /// <param name="id">The product ID.</param>
        /// <returns>The response containing the product.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                var product = _db.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == id); // Include Category
                _response.Result = _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="ProductDto">The product data transfer object.</param>
        /// <returns>The response containing the created product.</returns>
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Post(ProductDto ProductDto)
        {
            try
            {
                Product product = _mapper.Map<Product>(ProductDto);

                if (!_db.Categories.Any(c => c.CategoryId == product.CategoryId))
                {
                    _response.IsSuccess = false;
                    _response.Message = "Invalid Category ID.";
                    return _response;
                }

                _db.Products.Add(product);
                _db.SaveChanges();

                if (ProductDto.Image != null)
                {
                    string fileName = product.ProductId + Path.GetExtension(ProductDto.Image.FileName);
                    string filePath = @"wwwroot\ProductImages\" + fileName;

                    // Remove any existing image with the same name
                    var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    FileInfo file = new FileInfo(directoryLocation);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                    {
                        ProductDto.Image.CopyTo(fileStream);
                    }
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    product.ImageUrl = baseUrl + "/ProductImages/" + fileName;
                    product.ImageLocalPath = filePath;
                }
                else
                {
                    product.ImageUrl = "https://placehold.co/600x400";
                }
                _db.Products.Update(product);
                _db.SaveChanges();
                _response.Result = _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="ProductDto">The product data transfer object.</param>
        /// <returns>The response containing the updated product.</returns>
        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put(ProductDto ProductDto)
        {
            try
            {
                Product product = _mapper.Map<Product>(ProductDto);

                if (!_db.Categories.Any(c => c.CategoryId == product.CategoryId))
                {
                    _response.IsSuccess = false;
                    _response.Message = "Invalid Category ID.";
                    return _response;
                }

                if (ProductDto.Image != null)
                {
                    if (!string.IsNullOrEmpty(product.ImageLocalPath))
                    {
                        var oldFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), product.ImageLocalPath);
                        FileInfo file = new FileInfo(oldFilePathDirectory);
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }

                    string fileName = product.ProductId + Path.GetExtension(ProductDto.Image.FileName);
                    string filePath = @"wwwroot\ProductImages\" + fileName;
                    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                    {
                        ProductDto.Image.CopyTo(fileStream);
                    }
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    product.ImageUrl = baseUrl + "/ProductImages/" + fileName;
                    product.ImageLocalPath = filePath;
                }

                _db.Products.Update(product);
                _db.SaveChanges();

                _response.Result = _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        /// <summary>
        /// Deletes a product by ID.
        /// </summary>
        /// <param name="id">The product ID.</param>
        /// <returns>The response indicating the result of the delete operation.</returns>
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Product obj = _db.Products.First(u => u.ProductId == id);
                if (!string.IsNullOrEmpty(obj.ImageLocalPath))
                {
                    var oldFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), obj.ImageLocalPath);
                    FileInfo file = new FileInfo(oldFilePathDirectory);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
                _db.Products.Remove(obj);
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
