using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using SmartMenu.Services.ShoppingCartAPI.Controllers;
using SmartMenu.Services.ShoppingCartAPI.Data;
using SmartMenu.Services.ShoppingCartAPI.Models;
using SmartMenu.Services.ShoppingCartAPI.Models.Dto;
using SmartMenu.Services.ShoppingCartAPI.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMenu.UnitTests.CartAPI
{
    [TestFixture]
    public class CartAPIControllerTests
    {
        private AppDbContext _dbContext;
        private IMapper _mapper;
        private Mock<IProductService> _productServiceMock;
        private Mock<ICouponService> _couponServiceMock;
        private Mock<IConfiguration> _configurationMock;
        private CartAPIController _controller;

        /// <summary>
        /// Sets up the test environment before each test.
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
                cfg.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
                cfg.CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
            });
            _mapper = mapperConfig.CreateMapper();

            // Mock services
            _productServiceMock = new Mock<IProductService>();
            _couponServiceMock = new Mock<ICouponService>();
            _configurationMock = new Mock<IConfiguration>();

            // Initialize controller
            _controller = new CartAPIController(_dbContext, _mapper, _productServiceMock.Object, _couponServiceMock.Object, _configurationMock.Object);
        }

        /// <summary>
        /// Cleans up the test environment after each test.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        /// <summary>
        /// Tests the GetCart method to ensure it returns the cart when it exists.
        /// </summary>
        [Test]
        public async Task GetCart_WhenCartExists_ShouldReturnCart()
        {
            // Arrange
            _dbContext.CartHeaders.Add(new CartHeader { CartHeaderId = 1, UserId = "user1", CartTotal = 0 });
            _dbContext.CartDetails.Add(new CartDetails { CartDetailsId = 1, CartHeaderId = 1, ProductId = 101, Count = 2 });
            _dbContext.SaveChanges();

            _productServiceMock.Setup(p => p.GetProducts())
                .ReturnsAsync(new List<ProductDto> { new ProductDto { ProductId = 101, Price = 50 } });

            // Act
            var response = await _controller.GetCart("user1");

            // Assert
            Assert.IsTrue(response.IsSuccess, "Response should indicate success.");
            Assert.IsNotNull(response.Result, "Result should not be null.");
            var cart = response.Result as CartDto;
            Assert.AreEqual(1, cart.CartDetails.Count(), "Cart should contain one detail item.");
            Assert.AreEqual(100, cart.CartHeader.CartTotal, "Cart total should be calculated correctly.");
        }

        /// <summary>
        /// Tests the ApplyCoupon method to ensure it applies the coupon when it is valid.
        /// </summary>
        [Test]
        public async Task ApplyCoupon_WhenCouponValid_ShouldApplyCoupon()
        {
            // Arrange
            _dbContext.CartHeaders.Add(new CartHeader { CartHeaderId = 1, UserId = "user1", CartTotal = 100 });
            await _dbContext.SaveChangesAsync();

            _couponServiceMock.Setup(c => c.GetCoupon("SAVE10"))
                .ReturnsAsync(new CouponDto { CouponCode = "SAVE10", DiscountAmount = 10, MinAmount = 50 });

            var cartDto = new CartDto
            {
                CartHeader = new CartHeaderDto { UserId = "user1", CouponCode = "SAVE10", CartTotal = 100 }
            };

            // Act
            var responseObject = await _controller.ApplyCoupon(cartDto);
            var response = responseObject as ResponseDto; // Explicit cast to ResponseDto

            // Assert
            Assert.IsNotNull(response, "Response should not be null.");
            Assert.IsTrue(response.IsSuccess, "Response should indicate success.");
            Assert.IsTrue((bool)response.Result, "Coupon should be applied successfully.");
            var cartHeader = await _dbContext.CartHeaders.FirstOrDefaultAsync();
            Assert.AreEqual("SAVE10", cartHeader.CouponCode, "Coupon code should be applied to the cart header.");
        }

        /// <summary>
        /// Tests the CartUpsert method to ensure it creates a new cart when it does not exist.
        /// </summary>
        [Test]
        public async Task CartUpsert_WhenNewCart_ShouldCreateCart()
        {
            // Arrange
            var cartDto = new CartDto
            {
                CartHeader = new CartHeaderDto { UserId = "user1" },
                CartDetails = new List<CartDetailsDto>
                {
                    new CartDetailsDto { ProductId = 101, Count = 1 }
                }
            };

            // Act
            var response = await _controller.CartUpsert(cartDto);

            // Assert
            Assert.IsTrue(response.IsSuccess, "Response should indicate success.");
            var cartHeader = _dbContext.CartHeaders.FirstOrDefault();
            Assert.IsNotNull(cartHeader, "Cart header should be created.");
            var cartDetails = _dbContext.CartDetails.FirstOrDefault();
            Assert.IsNotNull(cartDetails, "Cart details should be created.");
        }

        /// <summary>
        /// Tests the IncreaseQuantity method to ensure it increases the quantity of a cart item.
        /// </summary>
        [Test]
        public async Task IncreaseQuantity_WhenCartItemExists_ShouldIncreaseQuantity()
        {
            // Arrange
            _dbContext.CartHeaders.Add(new CartHeader { CartHeaderId = 1, UserId = "user1" });
            _dbContext.CartDetails.Add(new CartDetails { CartDetailsId = 1, CartHeaderId = 1, ProductId = 101, Count = 1 });
            _dbContext.SaveChanges();

            // Act
            var response = await _controller.IncreaseQuantity(1);

            // Assert
            Assert.IsTrue(response.IsSuccess, "Response should indicate success.");
            var cartDetails = _dbContext.CartDetails.FirstOrDefault();
            Assert.AreEqual(2, cartDetails.Count, "Quantity should be increased by 1.");
        }

        /// <summary>
        /// Tests the DecreaseQuantity method to ensure it decreases the quantity of a cart item.
        /// </summary>
        [Test]
        public async Task DecreaseQuantity_WhenCartItemExists_ShouldDecreaseQuantity()
        {
            // Arrange
            _dbContext.CartHeaders.Add(new CartHeader { CartHeaderId = 1, UserId = "user1" });
            _dbContext.CartDetails.Add(new CartDetails { CartDetailsId = 1, CartHeaderId = 1, ProductId = 101, Count = 2 });
            _dbContext.SaveChanges();

            // Act
            var response = await _controller.DecreaseQuantity(1);

            // Assert
            Assert.IsTrue(response.IsSuccess, "Response should indicate success.");
            var cartDetails = _dbContext.CartDetails.FirstOrDefault();
            Assert.AreEqual(1, cartDetails.Count, "Quantity should be decreased by 1.");
        }

        /// <summary>
        /// Tests the RemoveCart method to ensure it removes a cart item.
        /// </summary>
        [Test]
        public async Task RemoveCart_WhenCartItemExists_ShouldRemoveCartItem()
        {
            // Arrange
            _dbContext.CartHeaders.Add(new CartHeader { CartHeaderId = 1, UserId = "user1" });
            _dbContext.CartDetails.Add(new CartDetails { CartDetailsId = 1, CartHeaderId = 1, ProductId = 101, Count = 1 });
            _dbContext.SaveChanges();

            // Act
            var response = await _controller.RemoveCart(1);

            // Assert
            Assert.IsTrue(response.IsSuccess, "Response should indicate success.");
            var cartDetails = _dbContext.CartDetails.FirstOrDefault();
            Assert.IsNull(cartDetails, "Cart item should be removed.");
        }

        /// <summary>
        /// Tests the ClearCart method to ensure it clears the cart for a specific user.
        /// </summary>
        [Test]
        public async Task ClearCart_WhenCartExists_ShouldClearCart()
        {
            // Arrange
            _dbContext.CartHeaders.Add(new CartHeader { CartHeaderId = 1, UserId = "user1" });
            _dbContext.CartDetails.Add(new CartDetails { CartDetailsId = 1, CartHeaderId = 1, ProductId = 101, Count = 1 });
            _dbContext.SaveChanges();

            // Act
            var response = await _controller.ClearCart("user1");

            // Assert
            Assert.IsTrue(response.IsSuccess, "Response should indicate success.");
            var cartHeader = _dbContext.CartHeaders.FirstOrDefault();
            Assert.IsNull(cartHeader, "Cart header should be removed.");
        }
    }
}
