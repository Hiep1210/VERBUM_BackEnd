using System;
using System.Collections.Generic;

namespace verbum_service_domain.Models
{
    public partial class Refreshtoken
    {
        public int TokenId { get; set; }
        public DateTime IssueAt { get; set; }
        public DateTime ExpireAt { get; set; }
        public string TokenContent { get; set; } = null!;
    }
}
