using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Mozilla;
using verbum_service_application.Service;
using verbum_service_domain.DTO.Request;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace verbum_service.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserService userService;
        public AuthenticationController(UserService userService)
        {
            this.userService = userService;
        }
        // GET: api/<AuthenticationController>
        [HttpGet]
        [Authorize]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AuthenticationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuthenticationController>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            var result = await userService.Login(userLogin);
            return result.IsSuccessed ? Ok(result) : BadRequest(result);
        }

        [HttpPost("signup")]
        public void SignUp([FromBody] string value)
        {
        }

        // PUT api/<AuthenticationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthenticationController>/5
        //[HttpDelete("{id}")]
        //public IActionResult Revoke()
        //{
        //    var username = User.Identity.Name;
        //    var user = _userContext.LoginModels.SingleOrDefault(u => u.UserName == username);
        //    if (user == null) return BadRequest();
        //    user.RefreshToken = null;
        //    _userContext.SaveChanges();
        //    return NoContent();
        //}
    }
}
