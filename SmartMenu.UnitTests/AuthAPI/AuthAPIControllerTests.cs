using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartMenu.Services.AuthAPI.Controllers;
using SmartMenu.Services.AuthAPI.Models.Dto;
using SmartMenu.Services.AuthAPI.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartMenu.UnitTests.AuthAPI
{
    [TestFixture]
    public class AuthAPIControllerTests
    {
        private Mock<IAuthService> _authServiceMock;
        private AuthAPIController _controller;

        [SetUp]
        public void SetUp()
        {
            _authServiceMock = new Mock<IAuthService>();
            _controller = new AuthAPIController(_authServiceMock.Object, null);
        }

        [Test]
        public async Task Register_WhenSuccessful_ShouldReturnOk()
        {
            // Arrange
            var registrationRequest = new RegistrationRequestDto
            {
                Email = "test@example.com",
                Password = "password123",
                Name = "Test User"
            };
            _authServiceMock.Setup(s => s.Register(registrationRequest)).ReturnsAsync(string.Empty);

            // Act
            var result = await _controller.Register(registrationRequest) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public async Task Register_WhenFailed_ShouldReturnBadRequest()
        {
            // Arrange
            var registrationRequest = new RegistrationRequestDto
            {
                Email = "test@example.com",
                Password = "password123",
                Name = "Test User"
            };
            _authServiceMock.Setup(s => s.Register(registrationRequest)).ReturnsAsync("Registration failed");

            // Act
            var result = await _controller.Register(registrationRequest) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public async Task Login_WhenSuccessful_ShouldReturnOk()
        {
            // Arrange
            var loginRequest = new LoginRequestDto { UserName = "testuser", Password = "password123" };
            var loginResponse = new LoginResponseDto
            {
                User = new UserDto { ID = "1", Email = "test@example.com", Name = "Test User" },
                Token = "fake-jwt-token"
            };
            _authServiceMock.Setup(s => s.Login(loginRequest)).ReturnsAsync(loginResponse);

            // Act
            var result = await _controller.Login(loginRequest) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var responseDto = result.Value as ResponseDto;
            Assert.IsNotNull(responseDto);
            Assert.IsTrue(responseDto.IsSuccess);
            Assert.AreEqual(loginResponse, responseDto.Result);
        }

        [Test]
        public async Task Login_WhenFailed_ShouldReturnBadRequest()
        {
            // Arrange
            var loginRequest = new LoginRequestDto { UserName = "testuser", Password = "wrongpassword" };
            _authServiceMock.Setup(s => s.Login(loginRequest)).ReturnsAsync(new LoginResponseDto());

            // Act
            var result = await _controller.Login(loginRequest) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public async Task AssignRole_WhenSuccessful_ShouldReturnOk()
        {
            // Arrange
            var assignRoleRequest = new RegistrationRequestDto { Email = "test@example.com", Role = "Admin" };
            _authServiceMock.Setup(s => s.AssignRole("test@example.com", "ADMIN")).ReturnsAsync(true);

            // Act
            var result = await _controller.AssignRole(assignRoleRequest) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public async Task AssignRole_WhenFailed_ShouldReturnBadRequest()
        {
            // Arrange
            var assignRoleRequest = new RegistrationRequestDto { Email = "test@example.com", Role = "Admin" };
            _authServiceMock.Setup(s => s.AssignRole("test@example.com", "ADMIN")).ReturnsAsync(false);

            // Act
            var result = await _controller.AssignRole(assignRoleRequest) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        private void MockUserClaims(string userId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = claimsPrincipal
                }
            };
        }

        [Test]
        public async Task EditAccount_WhenAuthorized_ShouldReturnOk()
        {
            // Arrange
            MockUserClaims("user1"); // Simulate logged-in user with ID "user1"
            var editAccountDto = new EditAccountDto { UserId = "user1", NewEmail = "new@example.com" };
            _authServiceMock.Setup(s => s.EditAccount(editAccountDto)).ReturnsAsync(true);

            // Act
            var result = await _controller.EditAccount(editAccountDto) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result, "Expected OkObjectResult.");
            Assert.AreEqual(200, result.StatusCode, "Expected HTTP 200 OK.");
        }

        [Test]
        public async Task EditAccount_WhenUnauthorized_ShouldReturnUnauthorized()
        {
            // Arrange
            MockUserClaims("user2"); // Simulate logged-in user with ID "user2"
            var editAccountDto = new EditAccountDto { UserId = "user1", NewEmail = "new@example.com" }; // User ID does not match logged-in user
            _authServiceMock.Setup(s => s.EditAccount(editAccountDto)).ReturnsAsync(false);

            // Act
            var result = await _controller.EditAccount(editAccountDto) as UnauthorizedObjectResult;

            // Assert
            Assert.IsNotNull(result, "Expected UnauthorizedObjectResult.");
            Assert.AreEqual(401, result.StatusCode, "Expected HTTP 401 Unauthorized.");
        }

        [Test]
        public async Task DeleteAccount_WhenSuccessful_ShouldReturnOk()
        {
            // Arrange
            MockUserClaims("user1"); // Simulate logged-in user with ID "user1"
            _authServiceMock.Setup(s => s.DeleteAccount("user1")).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteAccount("user1") as OkObjectResult;

            // Assert
            Assert.IsNotNull(result, "Expected OkObjectResult.");
            Assert.AreEqual(200, result.StatusCode, "Expected HTTP 200 OK.");
        }

        [Test]
        public async Task DeleteAccount_WhenUnauthorized_ShouldReturnUnauthorized()
        {
            // Arrange
            MockUserClaims("user2"); // Simulate logged-in user with ID "user2"
            var userIdToDelete = "user1"; // Attempt to delete account that doesn't match logged-in user
            _authServiceMock.Setup(s => s.DeleteAccount(userIdToDelete)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteAccount(userIdToDelete) as UnauthorizedObjectResult;

            // Assert
            Assert.IsNotNull(result, "Expected UnauthorizedObjectResult.");
            Assert.AreEqual(401, result.StatusCode, "Expected HTTP 401 Unauthorized.");
        }


    }
}
