using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace UrbanEngine.Web.UrbanEngineApi.VN.Controllers
{
    [Route("api/[controller]")]
    [ApiVersionNeutral]
    [ApiController]
    public class AboutController : ControllerBase
    { 
        private readonly ILogger _logger;

        public AboutController(ILogger<AboutController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var info = new
            {
                Service = new
                {
                    Name = GetShortName(),
                    Version = GetAssemblyVersion(),
                    ApiVersion = ApiVersion.Default.ToString()
                }
            };

            _logger.LogDebug("GetVersion - {info}", info);

            return Ok(info);
        }
         
        private string GetAssemblyVersion()
        {
            var assemblyName = Assembly.GetCallingAssembly().GetName();
            var version = assemblyName.Version.ToString();
            return version;
        }

        private string GetShortName()
        {
            var assemblyName = Assembly.GetCallingAssembly().GetName();
            var name = assemblyName.Name;
            var lastIdx = name.LastIndexOf('.');
            var shortName = name.Substring(lastIdx + 1);
            return shortName;
        }
    }
}