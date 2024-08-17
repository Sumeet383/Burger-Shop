using BurgerShop.BusinessLayer;
using BurgerShop.Controllers;
using BurgerShop.Models;
using BurgerShop.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace BurgerTests
{
    public class BurgerShopControllerTests
    {
        private readonly Mock<IBurgerShopService> _mockService;
        private readonly Mock<IBurgerRepository> _mockBurgerRepository; // Add this line
        private readonly BurgerShopController _controller;

        public BurgerShopControllerTests()
        {
            _mockService = new Mock<IBurgerShopService>();
            _mockBurgerRepository = new Mock<IBurgerRepository>(); // Initialize the mock
            _controller = new BurgerShopController(_mockService.Object, _mockBurgerRepository.Object) // Pass the mock
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
        }

        [Fact]
        public void Index_ReturnsUserList()
        {
            // Arrange
            var userList = new List<User>
            {
                new User { Id = 1, MobileNo = "1234567890", Password = "password1" },
                new User { Id = 2, MobileNo = "0987654321", Password = "password2" }
            };
            _mockService.Setup(service => service.GetAllUsers()).Returns(userList);

            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<User>>(viewResult.Model);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public void LoginPost_InvalidCredentials_ReturnsLoginViewWithError()
        {
            // Arrange
            _mockService.Setup(service => service.GetUserByMobileNo(It.IsAny<string>())).Returns((User)null);
            var controller = new BurgerShopController(_mockService.Object, _mockBurgerRepository.Object) // Pass the mock
            {
                TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>())
            };

            // Act
            var result = controller.LoginPost("1234567890", "wrongpassword");

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirectToActionResult.ActionName);
            Assert.Equal("Invalid mobile number or password.", controller.TempData["error"]);
        }

        [Fact]
        public void LoginPost_ValidCredentials_RedirectsToBurgerPage()
        {
            // Arrange
            var user = new User { Id = 1, MobileNo = "1234567890", Password = "hashedPassword" };
            _mockService.Setup(service => service.GetUserByMobileNo(It.IsAny<string>())).Returns(user);
            _mockService.Setup(service => service.VerifyPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _mockService.Setup(service => service.GenerateJwtToken(user, out It.Ref<DateTime>.IsAny))
            .Callback((User user, out DateTime expirationTime) =>
            {
                expirationTime = DateTime.UtcNow.AddHours(1);
            })
            .Returns("fake-jwt-token");

            // Act
            var result = _controller.LoginPost("1234567890", "correctpassword");

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("BurgerPage", redirectToActionResult.ActionName);
            Assert.Equal(user.Id, redirectToActionResult.RouteValues["id"]);
        }

        [Fact]
        public void BurgerPage_ValidUserId_ReturnsUserView()
        {
            // Arrange
            var user = new User { Id = 1, MobileNo = "1234567890", Password = "hashedPassword" };
            var menuList = new List<Menu>
    {
        new Menu { Id = 1, BurgerName = "Cheeseburger", BasePrice = 100, Description = "Delicious cheeseburger", ImageUrl = "cheeseburger.jpg" }
    };

            _mockService.Setup(service => service.GetUserById(It.IsAny<int>())).Returns(user);
            _mockBurgerRepository.Setup(repo => repo.GetAllBurgers()).Returns(menuList); // Mocking the burger repository

            // Act
            var result = _controller.BurgerPage(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<BurgerPageViewModel>(viewResult.Model); // Expecting BurgerPageViewModel
            Assert.Equal(user.Id, model.User.Id); // Check if the user ID matches
        }

        [Fact]
        public void RegisterPost_ValidUser_ReturnsRedirectToAction()
        {
            // Arrange
            var mockService = new Mock<IBurgerShopService>();
            var controller = new BurgerShopController(mockService.Object, _mockBurgerRepository.Object) // Pass the mock
            {
                TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>())
            };

            var user = new User { Id = 4, MobileNo = "9850964558", Password = "Sumeet" };
            mockService.Setup(service => service.GetUserByMobileNo(It.IsAny<string>())).Returns((User)null);

            // Act
            var result = controller.RegisterPost(user);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirectToActionResult.ActionName);
            Assert.Equal("User Registered Successfully", controller.TempData["success"]);
        }

        [Fact]
        public void ViewOrders_ValidUserId_ReturnsOrdersView()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { Id = 1, UserId = 1 },
                new Order { Id = 2, UserId = 1 }
            };
            var user = new User { Id = 1, MobileNo = "1234567890", Password = "password" };

            _mockService.Setup(service => service.GetUserById(It.IsAny<int>())).Returns(user);
            _mockService.Setup(service => service.GetOrdersByUserId(It.IsAny<int>())).Returns(orders);

            // Act
            var result = _controller.ViewOrders(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Order>>(viewResult.Model);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public void ViewOrders_InvalidUserId_ReturnsNotFound()
        {
            // Arrange
            _mockService.Setup(service => service.GetUserById(It.IsAny<int>())).Returns((User)null);

            // Act
            var result = _controller.ViewOrders(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("User not found.", notFoundResult.Value);
        }
    }
}