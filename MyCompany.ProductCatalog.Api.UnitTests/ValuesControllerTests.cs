using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCompany.ProductCatalog.Api.Controllers;
using MyCompany.ProductCatalog.Api.Database;
using System.Collections.Generic;
using System.Linq;

namespace MyCompany.ProductCatalog.Api.UnitTests
{
    [TestClass]
    public class ValuesControllerTests
    {
        [TestMethod]
        public void WhenCallingGet_ReturnsValidResponse()
        {
            // Arrange
            //var dbContextOptionsBuilder = new DbContextOptionsBuilder();
            //dbContextOptionsBuilder.UseInMemoryDatabase();
            //new DatabaseContext(dbContextOptionsBuilder);
            //var controller = new ProductsController(new DatabaseContext());

            //// Act
            //var result = controller.Get().Value.ToArray();

            //// Assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual("value1", result[0]);
            //Assert.AreEqual("value2", result[1]);
        }

        [TestMethod]
        public void WhenCallingGet_WithCorrectId_ReturnsValidProduct()
        {
            // Arrange
            //var controller = new ProductsController();

            //// Act
            //var result = controller.Get(1).Value.ToArray();

            //// Assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual("value1", result[0]);
        }
    }
}
