using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyCompany.ProductCatalog.Api.Controllers;
using MyCompany.ProductCatalog.Api.Database;
using MyCompany.ProductCatalog.Domain;
using System;

namespace MyCompany.ProductCatalog.Api.UnitTests
{
    [TestClass]
    public class ProudctsControllerTests
    {
        [TestMethod]
        public void WhenConstructing_WithNullDatabaseContext_ThrowsException()
        {
            // Arrange
            DatabaseContext dbContext = null;
            var mockLogger = new Mock<ILogger<ProductsController>>();

            // Act
            Action action = new Action(() => new ProductsController(dbContext, mockLogger.Object));

            // Assert
            Assert.ThrowsException<ArgumentNullException>(action);
        }

        [TestMethod]
        public void WhenConstructing_WithNullLogger_ThrowsException()
        {
            // Arrange
            DatabaseContext dbContext = CreateDbContext();
            ILogger<ProductsController> logger = null;

            // Act
            Action action = new Action(() => new ProductsController(dbContext, logger));

            // Assert
            Assert.ThrowsException<ArgumentNullException>(action);
        }

        [TestMethod]
        public void WhenCallingGet_WithoutParameters_ReturnsValidResponse()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ProductsController>>();
            var dbContext = BuildDBContext();

            var controller = new ProductsController(dbContext, mockLogger.Object);

            // Act
            var result = ((ObjectResult)controller.Get().Result.Result).Value;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [DataRow(1)]
        public void WhenCallingGet_WithCorrectId_ReturnsValidProduct(int id)
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ProductsController>>();
            var dbContext = BuildDBContext();
            var controller = new ProductsController(dbContext, mockLogger.Object);

            // Act
            var result = ((ObjectResult)controller.Get(id).Result.Result).Value as Product;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("HP", result.Code);
            Assert.AreEqual("Harry Potter", result.Name);
        }

        [TestMethod]
        public void WhenCallingPost_WithNullProduct_ShouldReturnServerError()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ProductsController>>();
            var dbContext = BuildDBContext();
            var controller = new ProductsController(dbContext, mockLogger.Object);

            // Act
            var result = (StatusCodeResult)controller.Post(null).Result;

            // Assert
            Assert.AreEqual(500, result.StatusCode);
        }

        [TestMethod]
        [DataRow(1)]
        public void WhenCallingPut_WithNullProduct_ThrowsException(int id)
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ProductsController>>();
            var dbContext = BuildDBContext();
            var controller = new ProductsController(dbContext, mockLogger.Object);

            // Act
            var result = (StatusCodeResult)controller.Put(id, null).Result;

            // Assert
            Assert.AreEqual(500, result.StatusCode);
        }

        [TestMethod]
        [DataRow(0)]
        public void WhenCallingGet_WithIncorrectId_ShouldReturnNotFound(int id)
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ProductsController>>();
            var dbContext = BuildDBContext();
            var controller = new ProductsController(dbContext, mockLogger.Object);

            // Act
            var result = (NotFoundResult)controller.Get(id).Result.Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        [DataRow(0)]
        public void WhenCallingDelete_WithCorrectId_ShouldNotThrow(int id)
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ProductsController>>();
            var dbContext = CreateDbContext();
            var controller = new ProductsController(dbContext, mockLogger.Object);

            // Act
            Action action = new Action(() => controller.Delete(id));

            // Assert
            AssertEx.NoExceptionThrown<Exception>(action);
        }

        private DatabaseContext BuildDBContext()
        {
            var dbContext = CreateDbContext();
            dbContext.Add(new Product() { Code = "HP", Name = "Harry Potter", Price = 12.5 });
            dbContext.SaveChanges();
            return dbContext;
        }

        private DatabaseContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("TestDB")
                .Options;

            var dbContext = new DatabaseContext(options);
            return dbContext;
        }
    }
    public sealed class AssertEx
    {
        public static void NoExceptionThrown<T>(Action a) where T : Exception
        {
            try
            {
                a();
            }
            catch (T)
            {
                Assert.Fail("Expected no {0} to be thrown", typeof(T).Name);
            }
        }
    }
}
