using System.Reflection; 
using Microsoft.AspNetCore.Mvc;

namespace UrbanEngine.Web.Controllers {
    [Route( "api/[controller]" )]
    [ApiController]
    public class AboutController : ControllerBase {
         
        [HttpGet]
        public IActionResult GetVersion() {
            return Ok( new {
                Service = new {
                    Name = GetShortName(), 
                    Version = GetAssemblyVersion()
                }
            } ); 
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