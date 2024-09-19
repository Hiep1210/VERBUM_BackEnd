using System;
using System.Collections.Generic;

namespace verbum_service_domain.Models
{
    public partial class Permission
    {
        public Permission()
        {
            UserCompanies = new HashSet<UserCompany>();
        }

        public string PermissionName { get; set; } = null!;
        public string Action { get; set; } = null!;
        public string Entity { get; set; } = null!;
        public int Id { get; set; }

        public virtual ICollection<UserCompany> UserCompanies { get; set; }
    }
}
