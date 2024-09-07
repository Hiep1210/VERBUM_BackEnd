using System;
using System.Collections.Generic;

namespace verbum_service_domain.Models
{
    public partial class ProjectSetting
    {
        public ProjectSetting()
        {
            Projects = new HashSet<Project>();
        }

        public int Id { get; set; }
        public bool PreTranslate { get; set; }
        public string FileNameFormat { get; set; } = null!;
        public string MarkedCompletedOn { get; set; } = null!;
        public string MarkedCanceledOn { get; set; } = null!;
        public string Workflow { get; set; } = null!;

        public virtual ICollection<Project> Projects { get; set; }
    }
}
