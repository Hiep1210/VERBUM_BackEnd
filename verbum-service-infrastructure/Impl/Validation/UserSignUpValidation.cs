using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using verbum_service_application.Validation;
using verbum_service_domain.Common.ErrorModel;
using verbum_service_domain.DTO.Request;
using verbum_service_domain.Utils;
using verbum_service_infrastructure.DataContext;

namespace verbum_service_infrastructure.Impl.Validation
{
    public class UserSignUpValidation : IValidation<UserSignUp>
    {
        private readonly verbumContext context;
        public UserSignUpValidation (verbumContext context)
        {
            this.context = context;
        }
        public async Task<List<string>> Validate(UserSignUp request)
        {
            List<string> alerts = new List<string>();
            await ValidateEmail(request, alerts);
            return alerts;
        }
        private async Task ValidateEmail(UserSignUp request, List<string> alerts)
        {
            if (await context.Users.AnyAsync(x => x.Email == request.Email))
            {
                alerts.Add(AlertMessage.Alert(ValidationAlertCode.DUPLICATE, "this email"));
            }
            if(!request.IsVerified)
            {
                alerts.Add(ValidationAlertCode.EMAIL_NOT_VERIFIED);
            }
        }
    }
}
