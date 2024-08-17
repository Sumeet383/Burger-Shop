using BurgerShop.Controllers;
using BurgerShop.Data;
using BurgerShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerShop.Tests
{
    public class BurgerShopControllerTests
    {
        [Fact]
        public void Index_ReturnsUserList()
        {
            // Arrange
            var mockDbContext = new Mock<ApplicationDbContext>();
            var mockConfiguration = new Mock<IConfiguration>();
            var controller = new BurgerShopController(mockDbContext.Object, mockConfiguration.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<User>>(viewResult.Model);
            Assert.NotEmpty(model);
        }

        [Fact]
        public void LoginPost_ValidMobileNoAndPassword_ReturnsRedirectToAction()
        {
            // Arrange
            var mockDbContext = new Mock<ApplicationDbContext>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockDbContext.Setup(db => db.Users.FirstOrDefault(It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>()))
                .Returns(new User { Id = 1, MobileNo = "1234567890", Password = "hashedPassword" });
            var controller = new BurgerShopController(mockDbContext.Object, mockConfiguration.Object);

            // Act
            var result = controller.LoginPost("9850964558", "Sumeet");

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("BurgerPage", redirectToActionResult.ActionName);
            Assert.Equal("BurgerShop", redirectToActionResult.ControllerName);
            Assert.Equal(1, redirectToActionResult.RouteValues["id"]);
        }

        [Fact]
        public void BurgerPage_ValidUserId_ReturnsUserView()
        {
            // Arrange
            var mockDbContext = new Mock<ApplicationDbContext>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockDbContext.Setup(db => db.Users.Find(It.IsAny<int>()))
                .Returns(new User { Id = 4, MobileNo = "9850964558", Password = "Sumeet" });
            var controller = new BurgerShopController(mockDbContext.Object, mockConfiguration.Object);

            // Act
            var result = controller.BurgerPage(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<User>(viewResult.Model);
            Assert.Equal("1234567890", model.MobileNo);
        }

        [Fact]
        public void RegisterPost_ValidUser_ReturnsRedirectToAction()
        {
            // Arrange
            var mockDbContext = new Mock<ApplicationDbContext>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockDbContext.Setup(db => db.Users.FirstOrDefault(It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>()))
                .Returns((User)null);
            var controller = new BurgerShopController(mockDbContext.Object, mockConfiguration.Object);
            var user = new User { Id = 4, MobileNo = "9850964558", Password = "Sumeet" };

            // Act
            var result = controller.RegisterPost(user);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirectToActionResult.ActionName);
            Assert.Equal("BurgerShop", redirectToActionResult.ControllerName);
        }

        [Fact]
        public void ViewOrders_ValidUserId_ReturnsOrdersView()
        {
           // Arrange
            var mockDbContext = new Mock<ApplicationDbContext>();
            var mockConfiguration = new Mock<IConfiguration>();

            // Mocking a user
            mockDbContext.Setup(db => db.Users.Find(It.IsAny<int>()))
                .Returns(new User { Id = 4 });

            // Mocking a list of orders for the user
            var ordersList = new List<Order>
            {
                new Order { Id = 13, UserId = 4 },
                new Order { Id = 14, UserId = 4 }
            };

            // Setting up the Orders property to return the list of orders
            mockDbContext.Setup(db => db.Orders.Where(It.IsAny<System.Linq.Expressions.Expression<Func<Order, bool>>>()))
                .Returns(ordersList.AsQueryable());

            var controller = new BurgerShopController(mockDbContext.Object, mockConfiguration.Object);

            // Act
            var result = controller.ViewOrders(4); // Pass the correct user ID

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Order>>(viewResult.Model);
            Assert.NotEmpty(model);
            Assert.Equal(2, model.Count); // Ensure that the returned model contains the correct number of orders
                }
    }
}
