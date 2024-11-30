using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartMenu.Services.ProductAPI.Controllers;
using SmartMenu.Services.ProductAPI.Data;
using SmartMenu.Services.ProductAPI.Models;
using SmartMenu.Services.ProductAPI.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMenu.UnitTests.ProductAPI
{
    /// <summary>
    /// Unit tests for the <see cref="ProductAPIController"/>.
    /// </summary>
    [TestFixture]
    public class ProductAPIControllerTests
    {
        private AppDbContext _dbContext;
        private IMapper _mapper;
        private ProductAPIController _controller;

        /// <summary>
        /// Sets up the test environment.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            // Configure in-memory database
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new AppDbContext(options);

            // Configure AutoMapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>().ReverseMap();
                cfg.CreateMap<Category, CategoryDto>().ReverseMap();
            });
            _mapper = mapperConfig.CreateMapper();

            // Initialize the controller
            _controller = new ProductAPIController(_dbContext, _mapper);

            // Seed Categories
            _dbContext.Categories.AddRange(
                new Category { CategoryId = 1, Name = "Category1" },
                new Category { CategoryId = 2, Name = "Category2" }
            );
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Cleans up the test environment.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            // Clean up the database after each test
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        /// <summary>
        /// Tests that the Get method returns all products.
        /// </summary>
        [Test]
        public void Get_WhenCalled_ShouldReturnAllProducts()
        {
            // Arrange
            _dbContext.Products.Add(new Product { ProductId = 1, Name = "Product1", CategoryId = 1, Description = "Description1" });
            _dbContext.Products.Add(new Product { ProductId = 2, Name = "Product2", CategoryId = 2, Description = "Description2" });
            _dbContext.SaveChanges();

            // Act
            var response = _controller.Get();

            // Assert
            Assert.IsTrue(response.IsSuccess, "Response should indicate success.");
            Assert.IsNotNull(response.Result, "Result should not be null.");
            var products = response.Result as IEnumerable<ProductDto>;
            Assert.IsNotNull(products, "Result should be of type IEnumerable<ProductDto>.");
            Assert.AreEqual(2, products.Count(), "Should return exactly 2 products.");
        }

        /// <summary>
        /// Tests that the Get method returns the correct product by ID.
        /// </summary>
        [Test]
        public void Get_WhenCalledWithValidId_ShouldReturnCorrectProduct()
        {
            // Arrange
            _dbContext.Products.Add(new Product { ProductId = 1, Name = "Product1", CategoryId = 1, Description = "Test Description" });
            _dbContext.SaveChanges();

            // Act
            var response = _controller.Get(1);

            // Assert
            Assert.IsTrue(response.IsSuccess, "Response should indicate success.");
            Assert.IsNotNull(response.Result, "Result should not be null.");
            var product = response.Result as ProductDto;
            Assert.IsNotNull(product, "Result should be of type ProductDto.");
            Assert.AreEqual("Product1", product.Name, "Product name should match.");
        }

        /// <summary>
        /// Tests that the Post method adds a new product.
        /// </summary>
        [Test]
        public void Post_WhenValidProductDtoProvided_ShouldAddNewProduct()
        {
            // Arrange
            var productDto = new ProductDto
            {
                Name = "NewProduct",
                CategoryId = 1,
                Price = 100,
                Description = "Test Description"
            };

            // Act
            var response = _controller.Post(productDto);

            // Assert
            Assert.IsTrue(response.IsSuccess, "Response should indicate success.");
            Assert.IsNotNull(response.Result, "Result should not be null.");
            var product = _dbContext.Products.FirstOrDefault();
            Assert.IsNotNull(product, "Product should be added to the database.");
            Assert.AreEqual("NewProduct", product.Name, "Product name should match.");
        }

        /// <summary>
        /// Tests that the Put method updates an existing product.
        /// </summary>
        [Test]
        public void Put_WhenValidProductDtoProvided_ShouldUpdateExistingProduct()
        {
            // Arrange
            _dbContext.Products.Add(new Product { ProductId = 1, Name = "OldProduct", CategoryId = 1, Description = "Test Description" });
            _dbContext.SaveChanges();

            var trackedEntity = _dbContext.Products.Local.FirstOrDefault(p => p.ProductId == 1);
            if (trackedEntity != null)
            {
                _dbContext.Entry(trackedEntity).State = EntityState.Detached;
            }

            var productDto = new ProductDto
            {
                ProductId = 1,
                Name = "UpdatedProduct",
                CategoryId = 1,
                Price = 200,
                Description = "Test Description"
            };

            // Act
            var response = _controller.Put(productDto);

            // Assert
            Assert.IsTrue(response.IsSuccess, "Response should indicate success.");
            var product = _dbContext.Products.FirstOrDefault();
            Assert.IsNotNull(product, "Product should still exist in the database.");
            Assert.AreEqual("UpdatedProduct", product.Name, "Product name should be updated.");
        }

        /// <summary>
        /// Tests that the Delete method removes a product by ID.
        /// </summary>
        [Test]
        public void Delete_WhenCalledWithValidId_ShouldRemoveProduct()
        {
            // Arrange
            _dbContext.Products.Add(new Product { ProductId = 1, Name = "ProductToDelete", CategoryId = 1, Description = "Test Description" });
            _dbContext.SaveChanges();

            // Act
            var response = _controller.Delete(1);

            // Assert
            Assert.IsTrue(response.IsSuccess, "Response should indicate success.");
            var product = _dbContext.Products.FirstOrDefault();
            Assert.IsNull(product, "Product should be removed from the database.");
        }
    }
}
