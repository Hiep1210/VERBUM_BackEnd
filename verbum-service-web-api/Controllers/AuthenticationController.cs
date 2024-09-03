using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using verbum_service_application.Service;
using verbum_service_domain.Common.ErrorModel;
using verbum_service_domain.DTO.Request;
using verbum_service_domain.DTO.Response;
using verbum_service_domain.Models;
using verbum_service_domain.Utils;
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

        // POST api/<AuthenticationController>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            return Ok(await userService.Login(userLogin));
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

        [HttpGet("confirm-email/{token}/{email}")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            string emailFromCookie = Request.Cookies[token];
            if (ObjectUtils.IsEmpty(emailFromCookie))
            {
                throw new BusinessException(ValidationAlertCode.EMAIL_EXPIRED);
            }
            if(emailFromCookie == email)
            {
                throw new BusinessException(AlertMessage.Alert(ValidationAlertCode.INVALID, "this email"));
            }
            return Ok(await userService.ConfirmEmail(token, email)); 
        }

        [HttpPost("resend-email")]
        public async Task<IActionResult> ResendVerficationEmail(string email)
        {
            await userService.SendConfirmationEmail(email);
            return NoContent();
        }
    }
}
