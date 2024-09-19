using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using verbum_service_domain.Models;

namespace verbum_service_application.Service
{
    public interface PermissionService
    {
        Task<List<Permission>> GetPermissionsForUser(Guid userId, Guid companyId);
    }
}
