using System;
using System.Collections.Generic;

namespace verbum_service_domain.Models
{
    public partial class CompanyProject
    {
        public int Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid ProjectId { get; set; }

        public virtual Company Company { get; set; } = null!;
        public virtual Project Project { get; set; } = null!;
    }
}
