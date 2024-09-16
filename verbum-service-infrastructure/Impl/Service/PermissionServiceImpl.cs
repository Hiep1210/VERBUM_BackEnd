using Lombok.NET;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using verbum_service_application.Service;
using verbum_service_domain.Models;
using verbum_service_infrastructure.DataContext;

namespace verbum_service_infrastructure.Impl.Service
{
    [RequiredArgsConstructor]
    public partial class PermissionServiceImpl : PermissionService
    {
        private readonly verbumContext verbumContext;
        public async Task<List<Permission>> GetPermissionsForUser(Guid userId, Guid companyId)
        {
            return await verbumContext.UserCompanies
                .Where(uc => uc.UserId == userId && uc.CompanyId == companyId)
                .SelectMany(uc => uc.PermissionNames)
                .ToListAsync();
        }
    }
}
