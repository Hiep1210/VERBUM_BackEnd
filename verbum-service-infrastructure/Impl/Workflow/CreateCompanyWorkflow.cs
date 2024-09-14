using AutoMapper;
using Lombok.NET;
using verbum_service_application.Service;
using verbum_service_application.Workflow;
using verbum_service_domain.Common.ErrorModel;
using verbum_service_domain.DTO.Request;
using verbum_service_domain.Models;
using verbum_service_domain.Utils;
using verbum_service_infrastructure.Impl.Validation;

namespace verbum_service_infrastructure.Impl.Workflow
{
    [RequiredArgsConstructor]
    public partial class CreateCompanyWorkflow : AbstractWorkFlow<CreateCompanyRequest>
    { 
        private readonly CompanyService companyService;
        private readonly IMapper mapper;
        private readonly CreateCompanyValidation createCompanyValidation;
        protected override async Task PreStep(CreateCompanyRequest request)
        {
        }

        protected override async Task ValidationStep(CreateCompanyRequest request)
        {
            List<string> alerts = await createCompanyValidation.Validate(request);
            if (ObjectUtils.IsNotEmpty(alerts))
            {
                throw new BusinessException(alerts);
            }
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
