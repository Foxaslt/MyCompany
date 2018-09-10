using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCompany.ProductCatalog.Api.Controllers;

namespace MyCompany.ProductCatalog.Api.UnitTests
{
    [TestClass]
    public class ControllerTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            ValuesController contoller = new ValuesController();

            // Act
            var result = contoller.Get(0);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
