using System.Reflection; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace UrbanEngine.Web.Controllers {
    /// <summary>
    /// identifies information about the service
    /// </summary>
    [ApiVersionNeutral]
    [Route( "[controller]" )]
    [ApiController]
    public class AboutController : ControllerBase {
        #region Injected Members

        private readonly ILogger _logger;

        #endregion

        /// <summary>
        /// constructor 
        /// </summary>
        /// <param name="logger"></param>
        public AboutController( ILogger<AboutController> logger ) {
            _logger = logger;
        }

        /// <summary>
        /// retrieve version of the service 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetVersion() {  
            var info = new {
                Service = new {
                    Name = GetShortName(),
                    Version = GetAssemblyVersion(),
                    ApiVersion = ApiVersion.Default.ToString()
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