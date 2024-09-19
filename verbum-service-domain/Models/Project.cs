using System;
using System.Collections.Generic;

namespace verbum_service_domain.Models
{
    public partial class Project
    {
        public Project()
        {
            Jobs = new HashSet<Job>();
            ProjectTargetLanguages = new HashSet<ProjectTargetLanguage>();
        }

        public Guid Id { get; set; }
        /// <summary>
        /// Name of a project
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// All jobs in this projects must be earlier than this date
        /// </summary>
        public DateTime DueDate { get; set; }
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Name of the client that all jobs in this project belongs to
        /// </summary>
        public string CilentName { get; set; } = null!;
        /// <summary>
        /// PMs or Admin who will manage this project
        /// </summary>
        public string OwnerName { get; set; } = null!;
        /// <summary>
        /// The domain that this project this about
        /// </summary>
        public string Domain { get; set; } = null!;
        /// <summary>
        /// The sub-domain of project&apos;s domain
        /// </summary>
        public string SubDomain { get; set; } = null!;
        /// <summary>
        /// The original language all jobs in this project
        /// </summary>
        public string SourceLanguageId { get; set; } = null!;
        /// <summary>
        /// The settings that all jobs in this project have to follow
        /// </summary>
        public int ProjectSettingId { get; set; }
        /// <summary>
        /// The QA options that all jobs in this project have to follow
        /// </summary>
        public int ProjectQaId { get; set; }
        public Guid? CompanyId { get; set; }

        public virtual Company? Company { get; set; }
        public virtual ProjectQa ProjectQa { get; set; } = null!;
        public virtual ProjectSetting ProjectSetting { get; set; } = null!;
        public virtual Language SourceLanguage { get; set; } = null!;
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<ProjectTargetLanguage> ProjectTargetLanguages { get; set; }
    }
}
