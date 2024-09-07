using System;
using System.Collections.Generic;

namespace verbum_service_domain.Models
{
    public partial class TargetLanguage
    {
        public int Id { get; set; }
        public Guid ProjectId { get; set; }

        public virtual Project Project { get; set; } = null!;
        public virtual Language? Language { get; set; }
    }
}
