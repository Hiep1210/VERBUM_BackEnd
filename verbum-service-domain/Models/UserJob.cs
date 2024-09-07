using System;
using System.Collections.Generic;

namespace verbum_service_domain.Models
{
    public partial class UserJob
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid JobId { get; set; }
        public Guid? WorkflowId { get; set; }

        public virtual Job Job { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual Workflow? Workflow { get; set; }
    }
}
