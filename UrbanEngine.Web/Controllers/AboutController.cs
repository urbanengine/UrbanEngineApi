using System.Reflection; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace UrbanEngine.Web.Controllers {
    [Route( "[controller]" )]
    [ApiController]
    public class AboutController : ControllerBase {
        #region Injected Members

        private readonly ILogger _logger;

        #endregion

        public AboutController( ILogger<AboutController> logger ) {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetVersion() {  
            var info = new {
                Service = new {
                    Name = GetShortName(),
                    Version = GetAssemblyVersion()
                }
            };

            _logger.LogDebug( "GetVersion - {info}", info );

            return Ok( info ); 
        }

        // TODO: we can move these out into a helper class to be reused across other services 

        private string GetAssemblyVersion() {
            var assemblyName = Assembly.GetCallingAssembly().GetName(); 
            var version = assemblyName.Version.ToString(); 
            return version;
        }

        private string GetShortName() {
            var assemblyName = Assembly.GetCallingAssembly().GetName();
            var name = assemblyName.Name;
            var lastIdx = name.LastIndexOf( '.' );
            var shortName = name.Substring( lastIdx + 1 );
            return shortName;
        }

    }
}