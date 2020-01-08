using Doctorly.CodeTest.Repository.DTOS;
using Doctorly.CodeTestRepository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Doctorly.CodeTestREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IConfiguration Configuration { get; }
        ILogger<UserController> Logger { get; }
        IUserRepository UserRepository { get; }
        public UserController(IConfiguration configuration, ILogger<UserController> logger, IUserRepository userRepository)
        {
            Configuration = configuration;
            Logger = logger;
            UserRepository = userRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var returnValue = await UserRepository.GetAllUsers();
            return Ok(returnValue);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var returnValue = await UserRepository.GetUser(id);
            return Ok(returnValue);
        }
        [HttpPost]
        public async Task<IActionResult> InsertUser([FromBody]UserDTO user)
        {
            var returnValue = await UserRepository.InsertUser(user);
            return Ok(returnValue);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserDTO user)
        {
            var returnValue = await UserRepository.UpdatetUser(user);
            return Ok(returnValue);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletetUser(int id)
        {

            var returnValue = await UserRepository.DeletetUser(id);
            return Ok(returnValue);
        }
    }
}