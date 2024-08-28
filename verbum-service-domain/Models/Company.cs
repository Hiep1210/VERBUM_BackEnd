using System;
using System.Collections.Generic;

namespace verbum_service_domain.Models
{
    public partial class Company
    {
        public Guid CompanyId { get; set; }
        public string Name { get; set; } = null!;
    }
}
