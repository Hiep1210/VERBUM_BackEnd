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

        public string LanguageName { get; set; } = null!;
        public string LanguageId { get; set; } = null!;

        public virtual Job? Job { get; set; }
        public virtual ProjectTargetLanguage? ProjectTargetLanguage { get; set; }
        public virtual Revelancy? RevelancySourceLanguage { get; set; }
        public virtual Revelancy? RevelancyTargetLanguage { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
