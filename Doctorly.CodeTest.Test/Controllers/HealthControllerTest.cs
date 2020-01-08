using Doctorly.CodeTest.Repository.DTOS;
using Doctorly.CodeTest.Test.Helper;
using Doctorly.CodeTestREST.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doctorly.CodeTest.Test.Controller
{

    [TestClass]
    public class HealthControllerTest
    {
        HealthController GetHealthControllerr()
        {
            Mock<IConfiguration> configuration = new Mock<IConfiguration>();
            Mock<ILogger<HealthController>> logger = new Mock<ILogger<HealthController>>();
            Mock<IOptions<ServiceConfigOptions>> serviceConfigAccessor = new Mock<IOptions<ServiceConfigOptions>>();
            serviceConfigAccessor.Setup(s => s.Value).Returns(ConcreteObjects.ServiceOptions);
            return new HealthController(configuration.Object, logger.Object, serviceConfigAccessor.Object);
        }
        [TestMethod]
        [TestCategory("Doctorly.CodeTest.REST"), TestCategory("Controllers"), TestCategory("Controller-Health")]
        public void HealthOK()
        {
            HealthController controller = GetHealthControllerr();
            var result = controller.Get() as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOfType(result.Value, typeof(ServiceConfigOptions));
            Assert.AreEqual(ConcreteObjects.ServiceOptions.ServiceName, (result.Value as ServiceConfigOptions).ServiceName);
        }
    }
}
