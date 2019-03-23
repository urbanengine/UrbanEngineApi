using Microsoft.AspNetCore.Mvc;

namespace UrbanEngineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                Name = nameof(UrbanEngineApi)
            });
        }
    }
}