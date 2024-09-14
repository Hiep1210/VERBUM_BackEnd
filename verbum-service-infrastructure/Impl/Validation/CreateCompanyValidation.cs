using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using verbum_service_application.Validation;
using verbum_service_domain.Common;
using verbum_service_domain.Common.ErrorModel;
using verbum_service_domain.DTO.Request;
using verbum_service_domain.Utils;

namespace verbum_service_infrastructure.Impl.Validation
{
    public class CreateCompanyValidation : IValidation<CreateCompanyRequest>
    {
        public Task<List<string>> Validate(CreateCompanyRequest request)
        {
            if(ObjectUtils.IsEmpty(request.Name) || !Regex.ALPHA_NUMERIC.IsMatch(request.Name))
            {
                return Task.FromResult(new List<string> { AlertMessage.Alert(ValidationAlertCode.INVALID, "Company Name") });
            }
            return Task.FromResult(new List<string>());
        }
    }
}
