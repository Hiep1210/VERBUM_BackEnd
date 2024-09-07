using System;
using System.Collections.Generic;

namespace verbum_service_domain.Models
{
    public partial class ProjectQa
    {
        public ProjectQa()
        {
            Projects = new HashSet<Project>();
        }

        public int Id { get; set; }
        public int QaOptions { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
