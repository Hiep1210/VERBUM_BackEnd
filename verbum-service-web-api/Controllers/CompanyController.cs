using Lombok.NET;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Cmp;
using System.Net;
using verbum_service.Filter;
using verbum_service_domain.Common.ErrorModel;
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
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorObject), 200)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddCompany(CreateCompanyRequest request)
        {
            await createCompanyWorkflow.process(request);
            return NoContent();
        }
    }
}
