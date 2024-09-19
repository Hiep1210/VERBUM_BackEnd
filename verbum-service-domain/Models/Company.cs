using System;
using System.Collections.Generic;

namespace verbum_service_domain.Models
{
    public partial class Company
    {
        public Company()
        {
            Projects = new HashSet<Project>();
            UserCompanies = new HashSet<UserCompany>();
        }

        public Guid CompanyId { get; set; }
        public string Name { get; set; } = null!;
        public bool Status { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<UserCompany> UserCompanies { get; set; }
    }
}
