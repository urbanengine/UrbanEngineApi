using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UrbanEngine.Core;
using UrbanEngine.Core.Interfaces;

namespace urban_engine_api.Controllers
{
    [Route("api/[controller]")]
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

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("user/{id}")]
        public ActionResult<string> Get(long id)
        {
            return "value";
        }

        [HttpGet("user")]
        public ActionResult<User> Get(string firstName, string lastName)
        {
            return new User()
            {
                FirstName = firstName,
                LastName = lastName
            };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
