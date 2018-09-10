using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCompany.ProductCatalog.Api.Controllers;
using MyCompany.ProductCatalog.Api.Database;
using MyCompany.ProductCatalog.Domain;

namespace MyCompany.ProductCatalog.Api.UnitTests
{
    [TestClass]
    public class ValuesControllerTests
    {
        [TestMethod]
        public void WhenCallingGet_WithoutParameters_ReturnsValidResponse()
        {
            // Arrange
            var dbContext = BuildDBContext();
            var controller = new ProductsController(dbContext);

            // Act
            var result = ((ObjectResult)controller.Get().Result.Result).Value;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void WhenCallingGet_WithCorrectId_ReturnsValidProduct()
        {
            // Arrange
            var dbContext = BuildDBContext();
            var controller = new ProductsController(dbContext);

            // Act
            var result = ((ObjectResult)controller.Get(1).Result.Result).Value as Product;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("HP", result.Code);
            Assert.AreEqual("Harry Potter", result.Name);
        }

        private DatabaseContext BuildDBContext()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("TestDB")
                .Options;

            var dbContext = new DatabaseContext(options);
            dbContext.Add(new Product() { Code = "HP", Name = "Harry Potter", Price = 12.5 });
            dbContext.SaveChanges();
            return dbContext;
        }
    }
}
