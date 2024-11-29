using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using SmartMenu.Services.CouponAPI.Controllers;
using SmartMenu.Services.CouponAPI.Data;
using SmartMenu.Services.CouponAPI.Models;
using SmartMenu.Services.CouponAPI.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMenu.UnitTests.CouponAPI
{
    [TestFixture]
    public class CouponAPIControllerTests
    {
        private AppDbContext _dbContext;
        private IMapper _mapper;
        private CouponAPIController _controller;

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
                cfg.CreateMap<Coupon, CouponDto>().ReverseMap();
            });
            _mapper = mapperConfig.CreateMapper();

            // Initialize controller
            _controller = new CouponAPIController(_dbContext, _mapper);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Test]
        public void Get_WhenCalled_ShouldReturnAllCoupons()
        {
            // Arrange
            _dbContext.Coupons.Add(new Coupon { CouponId = 1, CouponCode = "SAVE10", DiscountAmount = 10 });
            _dbContext.Coupons.Add(new Coupon { CouponId = 2, CouponCode = "SAVE20", DiscountAmount = 20 });
            _dbContext.SaveChanges();

            // Act
            var response = _controller.Get();

            // Assert
            Assert.IsTrue(response.IsSuccess, "Response should indicate success.");
            var coupons = response.Result as IEnumerable<CouponDto>;
            Assert.IsNotNull(coupons, "Result should not be null.");
            Assert.AreEqual(2, coupons.Count(), "All coupons should be returned.");
        }

        [Test]
        public void GetById_WhenCouponExists_ShouldReturnCoupon()
        {
            // Arrange
            _dbContext.Coupons.Add(new Coupon { CouponId = 1, CouponCode = "SAVE10", DiscountAmount = 10 });
            _dbContext.SaveChanges();

            // Act
            var response = _controller.Get(1);

            // Assert
            Assert.IsTrue(response.IsSuccess, "Response should indicate success.");
            var coupon = response.Result as CouponDto;
            Assert.IsNotNull(coupon, "Coupon should not be null.");
            Assert.AreEqual("SAVE10", coupon.CouponCode, "Coupon code should match.");
        }

        [Test]
        public void GetByCode_WhenCouponExists_ShouldReturnCoupon()
        {
            // Arrange
            _dbContext.Coupons.Add(new Coupon { CouponId = 1, CouponCode = "SAVE10", DiscountAmount = 10 });
            _dbContext.SaveChanges();

            // Act
            var response = _controller.GetByCode("SAVE10");

            // Assert
            Assert.IsTrue(response.IsSuccess, "Response should indicate success.");
            var coupon = response.Result as CouponDto;
            Assert.IsNotNull(coupon, "Coupon should not be null.");
            Assert.AreEqual(10, coupon.DiscountAmount, "Discount amount should match.");
        }

        


        [Test]
        public void Delete_WhenCouponDoesNotExist_ShouldReturnError()
        {
            // Act
            var response = _controller.Delete(99);

            // Assert
            Assert.IsFalse(response.IsSuccess, "Response should indicate failure for non-existing CouponId.");
            Assert.IsNotNull(response.Message, "Error message should be returned.");
        }
    }
}
