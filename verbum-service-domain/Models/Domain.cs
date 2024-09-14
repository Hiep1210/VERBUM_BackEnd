using System;
using System.Collections.Generic;

namespace verbum_service_domain.Models
{
    public partial class Domain
    {
        public Domain()
        {
            SubDomains = new HashSet<SubDomain>();
        }

        public Guid DomainId { get; set; }
        public string? DomainName { get; set; }

        public virtual ICollection<SubDomain> SubDomains { get; set; }
    }
}
