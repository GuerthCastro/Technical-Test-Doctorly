using Doctorly.CodeTest.Repository.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Doctorly.CodeTestREST.Controllers
{
    [Route("health")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        IConfiguration Configuration { get; }
        ILogger<HealthController> Logger { get; }
        IOptions<ServiceConfigOptions> ServiceConfigAccessor { get; }
        public HealthController(IConfiguration configuration, ILogger<HealthController> logger, IOptions<ServiceConfigOptions> serviceConfigAccessor)
        {
            Configuration = configuration;
            Logger = logger;
            ServiceConfigAccessor = serviceConfigAccessor;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(ServiceConfigAccessor.Value);
        }
    }
}
