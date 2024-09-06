using System;
using System.Collections.Generic;

namespace verbum_service_domain.Models
{
    public partial class Language
    {
        public Language()
        {
            Projects = new HashSet<Project>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual TargetLanguage IdNavigation { get; set; } = null!;
        public virtual ICollection<Project> Projects { get; set; }
    }
}
