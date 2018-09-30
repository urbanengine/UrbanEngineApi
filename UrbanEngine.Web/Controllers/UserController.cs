using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UrbanEngine.Core;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Interfaces;

namespace urban_engine_api.Controllers {
    /// <summary>
    /// manages user account in the system
    /// </summary>
    [ApiVersion( "1.0" )]
    [Route( "v{version:apiVersion}/[controller]" )] 
    [ApiController]
    public class UserController : Controller {
        #region Injected Members

        private readonly IUserManager _manager;
        private readonly ILogger _logger;

        #endregion

        #region Constructor

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="logger"></param>
        public UserController( IUserManager manager, ILogger<UserController> logger ) {
            _manager = manager;
            _logger = logger;
        }

        #endregion

        // GET user/
        /// <summary>
        /// retrieve a user by first name and last name
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<User> Get( string firstName, string lastName ) { 
            _logger.LogInformation( "GetUser - {firstName} {lastName}", firstName, lastName );

            return new User() { 
                
            };
        }

        // GET user/5
        /// <summary>
        /// retrieve a user by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet( "{id}" )]
        public ActionResult<string> Get( long id ) {
            return "value";
        }

        // POST user/values
        /// <summary>
        /// add a new user to the system
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post( [FromBody] User value ) {
        }

        // DELETE user/5
        /// <summary>
        /// remove a user from the system by id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete( "{id}" )]
        public void Delete( long id ) {
        }
    }
}
