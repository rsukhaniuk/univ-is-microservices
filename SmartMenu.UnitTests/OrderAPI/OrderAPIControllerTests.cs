using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using SmartMenu.Services.OrderAPI.Controllers;
using SmartMenu.Services.OrderAPI.Data;
using SmartMenu.Services.OrderAPI.Models;
using SmartMenu.Services.OrderAPI.Models.Dto;
using SmartMenu.Services.OrderAPI.Service.IService;
using SmartMenu.Services.OrderAPI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMenu.UnitTests.OrderAPI
{
    [TestFixture]
    public class OrderAPIControllerTests
    {
        private AppDbContext _dbContext;
        private IMapper _mapper;
        private Mock<IProductService> _productServiceMock;
        private Mock<IConfiguration> _configurationMock;
        private OrderAPIController _controller;

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
                cfg.CreateMap<OrderHeader, OrderHeaderDto>().ReverseMap();
                cfg.CreateMap<OrderDetails, OrderDetailsDto>().ReverseMap();
            });
            _mapper = mapperConfig.CreateMapper();

            // Mock services
            _productServiceMock = new Mock<IProductService>();
            _configurationMock = new Mock<IConfiguration>();

            // Initialize controller
            _controller = new OrderAPIController(_dbContext, _productServiceMock.Object, _mapper, _configurationMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        

        [Test]
        public void GetOrder_WhenOrderExists_ShouldReturnOrderDetails()
        {
            // Arrange
            _dbContext.OrderHeaders.Add(new OrderHeader { OrderHeaderId = 1, UserId = "user1" });
            _dbContext.SaveChanges();

            // Act
            var response = _controller.Get(1);

            // Assert
            Assert.IsTrue(response.IsSuccess, "Response should indicate success.");
            var order = response.Result as OrderHeaderDto;
            Assert.IsNotNull(order, "Order should be returned.");
            Assert.AreEqual(1, order.OrderHeaderId, "Order ID should match.");
        }



        [Test]
        public async Task UpdateOrderStatus_WhenCalled_ShouldUpdateOrderStatus()
        {
            // Arrange
            _dbContext.OrderHeaders.Add(new OrderHeader { OrderHeaderId = 1, UserId = "user1", Status = SD.Status_Pending });
            _dbContext.SaveChanges();

            // Act
            var response = await _controller.UpdateOrderStatus(1, SD.Status_Approved);

            // Assert
            Assert.IsTrue(response.IsSuccess, "Response should indicate success.");
            var orderHeader = _dbContext.OrderHeaders.FirstOrDefault();
            Assert.AreEqual(SD.Status_Approved, orderHeader.Status, "Order status should be updated.");
        }
    }
}
