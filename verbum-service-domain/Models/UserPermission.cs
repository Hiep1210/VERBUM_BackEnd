using System;
using System.Collections.Generic;

namespace verbum_service_domain.Models
{
    public partial class UserPermission
    {
        public long Id { get; set; }
        public int PermissionNameId { get; set; }
        public int UserCompanyId { get; set; }

        public virtual Permission PermissionName { get; set; } = null!;
        public virtual UserCompany UserCompany { get; set; } = null!;
    }
}
