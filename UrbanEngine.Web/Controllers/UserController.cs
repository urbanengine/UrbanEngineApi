using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UrbanEngine.Core;
using UrbanEngine.Core.Interfaces;

namespace urban_engine_api.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Fields

        private IUserManager _manager = null;

        #endregion

        #region Constructor

        public UserController(IUserManager manager)
        {
            _manager = manager;
        }

        #endregion

        // GET user/
        [HttpGet]
        public ActionResult<User> Get(string firstName, string lastName) {
            return new User() {
                FirstName = firstName,
                LastName = lastName
            };
        }

        // GET user/5
        [HttpGet("/{id}")]
        public ActionResult<string> Get(long id)
        {
            return "value";
        }

        // POST user/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // DELETE user/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
