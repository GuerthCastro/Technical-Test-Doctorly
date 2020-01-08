using Doctorly.CodeTest.Repository.DTOS;
using Doctorly.CodeTest.Test.Helper;
using Doctorly.CodeTestRepository.Interfaces;
using Doctorly.CodeTestREST.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doctorly.CodeTest.Test.Controller
{

    [TestClass]
    public class UserControllerTest
    {
        List<UserDTO> TestUsers { get; }
        public UserControllerTest(){
            TestUsers = ConcreteObjects.GetUsers() as List<UserDTO>;
        }
        UserController GetUserControllerAsync()
        {
            Mock<IConfiguration> configuration = new Mock<IConfiguration>();
            Mock<ILogger<UserController>> logger = new Mock<ILogger<UserController>>();
            Mock<IOptions<ServiceConfigOptions>> serviceConfigAccessor = new Mock<IOptions<ServiceConfigOptions>>();
            Mock<IUserRepository> userRepository = new Mock<IUserRepository>();
            userRepository.Setup(s => s.GetUser(It.IsAny<int>())).Returns(async (int id) =>
            {
                var user = TestUsers.FirstOrDefault(u => u.Id.Equals(id));
                if (user != null)
                {
                    return await Task.FromResult(user);
                }
                else
                {
                    var exception = new Exception($"The user with Id: {id} do not exist");
                    exception.Data.Add(520, null);
                    throw exception;
                }
            });
            userRepository.Setup(s => s.DeletetUser(It.IsAny<int>())).Returns(async (int id) =>
            {
                var user = TestUsers.FirstOrDefault(u => u.Id.Equals(id));
                if (user != null)
                {
                    TestUsers.Remove(user);
                    return await Task.FromResult(user);
                }
                else
                {
                    var exception = new Exception($"The user with Id: {id} do not exist");
                    exception.Data.Add(520, null);
                    throw exception;
                }                
            });
            userRepository.Setup(s=> s.InsertUser(It.IsAny<UserDTO>())).Returns(async (UserDTO newUser) =>
            {
                newUser.Id = TestUsers.Max(u=> u.Id) +1;
                TestUsers.Add(newUser);
                return await Task.FromResult(newUser);

            });
            serviceConfigAccessor.Setup(s => s.Value).Returns(ConcreteObjects.ServiceOptions);
            return new UserController(configuration.Object, logger.Object, userRepository.Object);
        }
        [TestMethod]
        [TestCategory("Doctorly.CodeTest.REST"), TestCategory("Controllers"), TestCategory("Controller-User")]
        public async Task GetUserOk()
        {
            int userId = 1;
            UserController controller = GetUserControllerAsync();
            var expected = ConcreteObjects.GetUsers().FirstOrDefault(u => u.Id.Equals(userId));
            var actual = await  controller.GetUser(userId) as OkObjectResult;
            Assert.IsNotNull(actual);
            Assert.AreEqual(200, actual.StatusCode);
            Assert.IsInstanceOfType(actual.Value, typeof(UserDTO));
            Assert.AreEqual(expected.FirstName, (actual.Value as UserDTO).FirstName);
        }
        [TestMethod]
        [TestCategory("Doctorly.CodeTest.REST"), TestCategory("Controllers"), TestCategory("Controller-User")]
        public async Task GetUserNotExist()
        {
            int userId = 100;
            try
            {
                UserController controller = GetUserControllerAsync();
                var actual = await controller.GetUser(userId) as OkObjectResult;
                Assert.Fail("no exception thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is Exception);
                Assert.AreEqual(ex.Message, $"The user with Id: {userId} do not exist");
            }      
        }
        [TestMethod]
        [TestCategory("Doctorly.CodeTest.REST"), TestCategory("Controllers"), TestCategory("Controller-User")]
        public async Task DeleteUserOk()
        {
            int userId = 1;
            int userCount = TestUsers.Count;
            UserController controller = GetUserControllerAsync();
            var actual = await controller.DeletetUser(userId) as OkObjectResult;
            Assert.IsNotNull(actual);
            Assert.AreEqual(200, actual.StatusCode);
            Assert.IsNotNull(actual.Value);
            Assert.AreEqual(userCount-1, TestUsers.Count);
        }
        [TestMethod]
        [TestCategory("Doctorly.CodeTest.REST"), TestCategory("Controllers"), TestCategory("Controller-User")]
        public async Task DeleteUserNotExis()
        {
            int userId = 100;
            try
            {
                UserController controller = GetUserControllerAsync();
                var actual = await controller.DeletetUser(userId) as OkObjectResult;
                Assert.Fail("no exception thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is Exception);
                Assert.AreEqual(ex.Message, $"The user with Id: {100} do not exist");
            }
        }
        [TestMethod]
        [TestCategory("Doctorly.CodeTest.REST"), TestCategory("Controllers"), TestCategory("Controller-User")]
        public async Task InsertUserOk()
        {
            UserController controller = GetUserControllerAsync();
            int userCount = TestUsers.Count;
            var actual = await controller.InsertUser(new UserDTO { Id = 0, FirstName = "Test User running Test", LastName = "Last Name" }) as OkObjectResult;
            Assert.IsNotNull(actual);
            Assert.AreEqual(200, actual.StatusCode);
            Assert.IsInstanceOfType(actual.Value, typeof(UserDTO));
            Assert.AreEqual(userCount + 1, TestUsers.Count);
        }
    }
}
