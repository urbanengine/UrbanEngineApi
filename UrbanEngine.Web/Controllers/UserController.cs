using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UrbanEngine.Core;
using UrbanEngine.Core.Interfaces;

namespace urban_engine_api.Controllers {
    [Route( "user" )]
    [ApiController]
    public class UserController : Controller {
        #region Injected Members

        private readonly IUserManager _manager;
        private readonly ILogger _logger;

        #endregion

        #region Constructor

        public UserController( IUserManager manager, ILogger<UserController> logger ) {
            _manager = manager;
            _logger = logger;
        }

        #endregion

        // GET user/
        [HttpGet]
        public ActionResult<User> Get( string firstName, string lastName ) { 
            _logger.LogInformation( "GetUser - {firstName} {lastName}", firstName, lastName );

            return new User() {
                FirstName = firstName,
                LastName = lastName
            };
        }

        // GET user/5
        [HttpGet( "/{id}" )]
        public ActionResult<string> Get( long id ) {
            return "value";
        }

        // POST user/values
        [HttpPost]
        public void Post( [FromBody] string value ) {
        }

        // DELETE user/5
        [HttpDelete( "{id}" )]
        public void Delete( int id ) {
        }
    }
}
