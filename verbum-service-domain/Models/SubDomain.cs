using System;
using System.Collections.Generic;

namespace verbum_service_domain.Models
{
    public partial class SubDomain
    {
        public SubDomain()
        {
            Revelancies = new HashSet<Revelancy>();
        }

        public Guid SubDomainId { get; set; }
        public string? SubDomainName { get; set; }
        public Guid? DomainId { get; set; }

        public virtual Domain? Domain { get; set; }
        public virtual ICollection<Revelancy> Revelancies { get; set; }
    }
}
