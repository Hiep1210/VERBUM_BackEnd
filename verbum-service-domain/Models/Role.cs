﻿using System;
using System.Collections.Generic;

namespace verbum_service_domain.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
