using System;
using System.Collections.Generic;

namespace verbum_service_domain.Models
{
    public partial class UserCompany
    {
        public UserCompany()
        {
            PermissionNames = new HashSet<Permission>();
        }

        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
        public string Role { get; set; } = null!;
        public string Status { get; set; } = null!;

        public virtual Company Company { get; set; } = null!;
        public virtual Role RoleNavigation { get; set; } = null!;
        public virtual User User { get; set; } = null!;

        public virtual ICollection<Permission> PermissionNames { get; set; }
    }
}
