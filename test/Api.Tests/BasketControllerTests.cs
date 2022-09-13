using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Api.Controllers;
using Checkout.Core.Models;
using Checkout.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Api.Tests
{
    public class BasketControllerTests
    {
        private  Mock<BasketArticleRepositoryFake> _basketArticleRepositoryMock;
        private  Mock<BasketRepositoryFake> _basketRepositoryMock;
        private  Mock<ILogger<BasketController>> _loggerMock;
        private  BasketController _controller;

        public BasketControllerTests()
        {
           
        }

        public void InitMocks()
        {
            _loggerMock = new Mock<ILogger<BasketController>>();
            _basketArticleRepositoryMock = new Mock<BasketArticleRepositoryFake>();
            _basketRepositoryMock = new Mock<BasketRepositoryFake>(_basketArticleRepositoryMock.Object);
            var bServices = new BasketServices();
            _controller = new BasketController(_loggerMock.Object, _basketRepositoryMock.Object, _basketArticleRepositoryMock.Object, bServices);
        }


        [Fact]
        public void GetById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            InitMocks();
            // Act
            var notFoundResult = _controller.Get(-1);
            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void GetById_ExistingIdPassed_ReturnsOkResult()
        {
            InitMocks();

            // Arrange
            var testId = 1;
            // Act
            var okResult = _controller.Get(testId);
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result as OkObjectResult);
        }

        [Fact]
        public void GetById_ExistingIdPassed_ReturnsRightItem()
        {
            InitMocks();

            // Arrange
            var testId = 1;
            // Act
            var okResult = _controller.Get(testId).Result as OkObjectResult;
            // Assert
            Assert.IsType<Basket>(okResult.Value);
            Assert.Equal(testId, (okResult.Value as Basket).Id);
        }

        [Fact]
        public void Add_InvalidBasketIdPassed_ReturnsBadRequest()
        {
            InitMocks();
            // Arrange
            var customerMissingItem = new BasketDTO()
            {
                PaysVat = true
            };
            _controller.ModelState.AddModelError("Customer", "Required"); 
            // Act
            var badResponse = _controller.Post(customerMissingItem);
            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
        }

        [Fact]
        public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            InitMocks();
            // Arrange
            var testItem = new BasketDTO()
            {
                Customer = "customerTest",
                PaysVat = true
            };
            // Act
            var createdResponse = _controller.Post(testItem).Result as OkObjectResult;
            var item = createdResponse.Value as BasketDTO;
            // Assert
            Assert.IsType<BasketDTO>(item);
            Assert.Equal("customerTest", item.Customer);
            Assert.Equal(true, item.PaysVat);
        }

        [Fact]
        public void Update_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            InitMocks();
            // Arrange
            int basketId = 1;
            var testItem = new BasketArticle()
            {
               Item="item1",
               Price = 10
            };
            // Act
            var createdResponse = _controller.Put(basketId, testItem).Result as OkObjectResult;
            var item = createdResponse.Value as BasketArticle;
            // Assert
            Assert.IsType<BasketArticle>(item);
            Assert.Equal("item1", item.Item);
            Assert.Equal(10, item.Price);
        }

    }
}