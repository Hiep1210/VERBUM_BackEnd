using System;
using System.Collections.Generic;

namespace verbum_service_domain.Models
{
    public partial class Workflow
    {
        public Workflow()
        {
            UserJobs = new HashSet<UserJob>();
        }

        public Guid WorkflowId { get; set; }
        public string WorkflowName { get; set; } = null!;

        public virtual ICollection<UserJob> UserJobs { get; set; }
    }
}
