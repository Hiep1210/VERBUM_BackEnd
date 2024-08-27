using System;
using System.Collections.Generic;

namespace verbum_service_domain.Models
{
    public partial class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; } = null!;
        public DateTime? EmailVerified { get; set; }
        public int? ImageId { get; set; }
        public string RoleName { get; set; } = null!;
        public string? Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Status { get; set; }

        public virtual Image? Image { get; set; }
        public virtual Role? NameNavigation { get; set; }
    }
}
