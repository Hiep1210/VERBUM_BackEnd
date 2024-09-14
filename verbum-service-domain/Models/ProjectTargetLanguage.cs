using System;
using System.Collections.Generic;

namespace verbum_service_domain.Models
{
    public partial class ProjectTargetLanguage
    {
        public Guid ProjectId { get; set; }
        public int Id { get; set; }
        public string LanguageId { get; set; } = null!;

        public virtual Language Language { get; set; } = null!;
        public virtual Project Project { get; set; } = null!;
    }
}
