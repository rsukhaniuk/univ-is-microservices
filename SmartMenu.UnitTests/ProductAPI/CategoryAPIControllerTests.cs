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
    [TestFixture]
    public class CategoryAPIControllerTests
    {
        private AppDbContext _dbContext;
        private IMapper _mapper;
        private CategoryAPIController _controller;

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
                cfg.CreateMap<Category, CategoryDto>().ReverseMap();
            });
            _mapper = mapperConfig.CreateMapper();

            // Initialize the controller
            _controller = new CategoryAPIController(_dbContext, _mapper);

            // Seed data
            _dbContext.Categories.AddRange(
                new Category { CategoryId = 1, Name = "Category1" },
                new Category { CategoryId = 2, Name = "Category2" }
            );
            _dbContext.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up the database after each test
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Test]
        public void Get_WhenCalled_ShouldReturnAllCategories()
        {
            // Act
            var response = _controller.Get();

            // Assert
            Assert.IsTrue(response.IsSuccess, "Response should indicate success.");
            Assert.IsNotNull(response.Result, "Result should not be null.");
            var categories = response.Result as IEnumerable<CategoryDto>;
            Assert.IsNotNull(categories, "Result should be of type IEnumerable<CategoryDto>.");
            Assert.AreEqual(2, categories.Count(), "Should return exactly 2 categories.");
        }

        [Test]
        public void Get_WhenCalledWithValidId_ShouldReturnCorrectCategory()
        {
            // Act
            var response = _controller.Get(1);

            // Assert
            Assert.IsTrue(response.IsSuccess, "Response should indicate success.");
            Assert.IsNotNull(response.Result, "Result should not be null.");
            var category = response.Result as CategoryDto;
            Assert.IsNotNull(category, "Result should be of type CategoryDto.");
            Assert.AreEqual("Category1", category.Name, "Category name should match.");
        }

        [Test]
        public void Post_WhenValidCategoryDtoProvided_ShouldAddNewCategory()
        {
            // Arrange
            var categoryDto = new CategoryDto
            {
                Name = "NewCategory"
            };

            // Act
            var response = _controller.Post(categoryDto);

            // Assert
            Assert.IsTrue(response.IsSuccess, "Response should indicate success.");
            Assert.IsNotNull(response.Result, "Result should not be null.");
            var category = _dbContext.Categories.FirstOrDefault(c => c.Name == "NewCategory");
            Assert.IsNotNull(category, "Category should be added to the database.");
            Assert.AreEqual("NewCategory", category.Name, "Category name should match.");
        }

        [Test]
        public void Put_WhenValidCategoryDtoProvided_ShouldUpdateExistingCategory()
        {
            // Arrange
            _dbContext.Categories.Add(new Category { CategoryId = 3, Name = "OldCategory" });
            _dbContext.SaveChanges();

            // Detach tracked entity if necessary
            var trackedEntity = _dbContext.Categories.Local.FirstOrDefault(c => c.CategoryId == 3);
            if (trackedEntity != null)
            {
                _dbContext.Entry(trackedEntity).State = EntityState.Detached;
            }

            var categoryDto = new CategoryDto
            {
                CategoryId = 3,
                Name = "UpdatedCategory"
            };

            // Act
            var response = _controller.Put(categoryDto);

            // Assert
            Assert.IsTrue(response.IsSuccess, "Response should indicate success.");
            var category = _dbContext.Categories.FirstOrDefault(c => c.CategoryId == 3);
            Assert.IsNotNull(category, "Category should still exist in the database.");
            Assert.AreEqual("UpdatedCategory", category.Name, "Category name should be updated.");
        }

        [Test]
        public void Delete_WhenCalledWithValidId_ShouldRemoveCategory()
        {
            // Arrange
            _dbContext.Categories.Add(new Category { CategoryId = 3, Name = "CategoryToDelete" });
            _dbContext.SaveChanges();

            // Act
            var response = _controller.Delete(3);

            // Assert
            Assert.IsTrue(response.IsSuccess, "Response should indicate success.");
            var category = _dbContext.Categories.FirstOrDefault(c => c.CategoryId == 3);
            Assert.IsNull(category, "Category should be removed from the database.");
        }
    }
}
