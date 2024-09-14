using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using verbum_service_application.Service;
using verbum_service_domain.DTO.Request;
using verbum_service_domain.DTO.Response;
using verbum_service_infrastructure.Impl.Workflow;

namespace verbum_service.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;
        private readonly UpdateUserWorkflow updateUserWorkflow;
        public UserController(UserService userService, UpdateUserWorkflow updateUserWorkflow)
        {
            this.userService = userService;
            this.updateUserWorkflow = updateUserWorkflow;
        }

        [HttpGet("GetAllUserInCompany/{cid}")]
        [EnableQuery]
        public async Task<List<UserInfo>> GetAllUserInCompany(Guid cid)
        {
            return await userService.GetAllUserInCompany(cid);
        }

        [HttpGet("GetUserInCompanyById/{uid}/{cid}")]
        public async Task<UserInfo> GetUserInCompanyById(Guid uid,Guid cid)
        {
            return await userService.GetUserInCompanyById(uid,cid);
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserUpdate userUpdate)
        {
            await updateUserWorkflow.process(userUpdate);
            return Ok();
        }

        [HttpPut("UpdateUserCompanyStatus")]
        public async Task<IActionResult> UpdateUserCompanyStatus(Guid uid, Guid cid)
        {
            await userService.UpdateUserCompanyStatus(uid,cid);
            return Ok();
        }
    }
}
