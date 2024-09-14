using System;
using System.Collections.Generic;

namespace verbum_service_domain.Models
{
    public partial class Revelancy
    {
        public Guid RevelancyId { get; set; }
        public Guid? SubDomainId { get; set; }
        public Guid? UserId { get; set; }
        public string? SourceLanguageId { get; set; }
        public string? TargetLanguageId { get; set; }

        public virtual Language? SourceLanguage { get; set; }
        public virtual SubDomain? SubDomain { get; set; }
        public virtual Language? TargetLanguage { get; set; }
        public virtual User? User { get; set; }
    }
}
