using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using verbum_service.Filter;
using verbum_service_application.Service;
using verbum_service_domain.Common.Permission;
using verbum_service_domain.DTO.Request;
using verbum_service_domain.DTO.Response;
using verbum_service_infrastructure.Impl.Workflow;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace verbum_service.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserService userService;
        private readonly CreateUserWorkflow createUserWorkflow;
        public AuthenticationController(UserService userService, CreateUserWorkflow createUserWorkflow)
        {
            this.userService = userService;
            this.createUserWorkflow = createUserWorkflow;
        }
        // GET: api/<AuthenticationController>
        [HttpGet]
        [Authorize]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        [Authorize]
        [PermissionAuth(Entity.USER, ActionEnum.MANAGE)]
        public IEnumerable<string> Get(int id)
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<AuthenticationController>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            return Ok(await userService.Login(userLogin));
        }

        [HttpGet("google-login")]
        public IActionResult LoginGoogle()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/api/auth/google-callback" }, 
                GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("google-callback")]
        public async Task<IActionResult> GoogleCallback()
        {
            return Ok(await userService.LoginGoogleCallback());
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserSignUp userSignUp)
        {
            await createUserWorkflow.process(userSignUp);
            return NoContent();
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> Refresh([FromBody] Tokens tokens)
        {
            return Ok(await userService.RefreshAccessToken(tokens));
        }

        [HttpGet("confirm-email")]
        [Authorize]
        public async Task<IActionResult> ConfirmEmail([FromQuery, Required] string access_token)
        {
            string email = User.FindFirst(ClaimTypes.Email)?.Value ?? "";
            return Ok(await userService.ConfirmEmail(email)); 
        }

        [HttpPost("resend-email")]
        public async Task<IActionResult> ResendVerficationEmail(string email)
        {
            await userService.SendConfirmationEmail(email);
            return NoContent();
        }
    }
}
