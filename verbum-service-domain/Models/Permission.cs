﻿using System;
using System.Collections.Generic;

namespace verbum_service_domain.Models
{
    public partial class Permission
    {
        public Permission()
        {
            UserPermissions = new HashSet<UserPermission>();
        }

        public int Id { get; set; }
        public string PermissionName { get; set; } = null!;
        public string Action { get; set; } = null!;
        public string Entity { get; set; } = null!;

        public virtual ICollection<UserPermission> UserPermissions { get; set; }
    }
}
