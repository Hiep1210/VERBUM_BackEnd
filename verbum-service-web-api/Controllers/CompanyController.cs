using Lombok.NET;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using verbum_service.Filter;
using verbum_service_domain.DTO.Request;
using verbum_service_infrastructure.Impl.Workflow;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace verbum_service.Controllers
{
    [Route("api/company")]
    [ApiController]
    [Authorize]
    [RequiredArgsConstructor]
    public partial class CompanyController : ControllerBase
    {
        private readonly CreateCompanyWorkflow createCompanyWorkflow;
        [HttpPost]
        public async Task<IActionResult> AddCompany(CreateCompanyRequest request)
        {
            await createCompanyWorkflow.process(request);
            return Ok();
        }
    }
}
