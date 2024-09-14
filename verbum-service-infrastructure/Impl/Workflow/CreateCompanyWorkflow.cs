using AutoMapper;
using Lombok.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using verbum_service_application.Service;
using verbum_service_application.Workflow;
using verbum_service_domain.DTO.Request;
using verbum_service_domain.Models;

namespace verbum_service_infrastructure.Impl.Workflow
{
    [RequiredArgsConstructor]
    public partial class CreateCompanyWorkflow : AbstractWorkFlow<CreateCompanyRequest>
    { 
        private readonly CompanyService companyService;
        private readonly IMapper mapper;
        protected override async Task PreStep(CreateCompanyRequest request)
        {
        }

        protected override async Task ValidationStep(CreateCompanyRequest request)
        {
        }

        protected override async Task CommonStep(CreateCompanyRequest request)
        {
        }

        protected override async Task PostStep(CreateCompanyRequest request)
        {
            Company company = mapper.Map<Company>(request);
            company.CompanyId = Guid.NewGuid();
            await companyService.AddCompany(company);
        }
    }
}
