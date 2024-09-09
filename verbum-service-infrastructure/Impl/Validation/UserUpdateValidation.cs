using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using verbum_service_domain.Common.ErrorModel;
using verbum_service_domain.DTO.Request;
using verbum_service_domain.Utils;
using verbum_service_infrastructure.DataContext;

namespace verbum_service_infrastructure.Impl.Validation
{
    public class UserUpdateValidation
    {
        private readonly verbumContext context;
        public UserUpdateValidation(verbumContext context)
        {
            this.context = context;
        }
        public async Task<List<string>> Validate(UserUpdate request)
        {
            List<string> alerts = new List<string>();
            return alerts;
        }

        private async Task ValidateEmail(UserUpdate request, List<string> alerts)
        {
            if (!ValidationUtils.IsValidEmail(request.Data.Email))
            {
                alerts.Add(AlertMessage.Alert(ValidationAlertCode.INVALID, "this email"));
            }
            if (await context.Users.AnyAsync(x => x.Email == request.Data.Email && x.Id != request.UserId))
            {
                alerts.Add(AlertMessage.Alert(ValidationAlertCode.DUPLICATE, "this email"));
            }
        }

        private async Task ValidateUserCompany(UserUpdate request, List<string> alerts)
        {
            if(!await context.Users.AnyAsync(u => u.Id == request.UserId))
            {
                alerts.Add(AlertMessage.Alert(ValidationAlertCode.NOT_FOUND, "user"));
            }
            if(!await context.Companies.AnyAsync(c => c.CompanyId == request.CompanyId))
            {
                alerts.Add(AlertMessage.Alert(ValidationAlertCode.NOT_FOUND, "company"));
            }
            if(!await context.UserCompanies.AnyAsync(u => u.UserId == request.UserId && u.CompanyId == request.CompanyId))
            {
                alerts.Add(AlertMessage.Alert(ValidationAlertCode.NOT_FOUND, "usercomapny"));
            }
        }
    }
}
