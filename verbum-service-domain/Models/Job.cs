using System;
using System.Collections.Generic;

namespace verbum_service_domain.Models
{
    public partial class Job
    {
        public Job()
        {
            UserJobs = new HashSet<UserJob>();
        }

        /// <summary>
        /// The id of the job
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// The name of the job&apos;s document
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// The status of the job
        /// </summary>
        public long Status { get; set; }
        /// <summary>
        /// Job have to finish before this date
        /// </summary>
        public DateTime DueDate { get; set; }
        /// <summary>
        /// The date time that the jobs was created
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// The date time that the jobs was updated
        /// </summary>
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// The number of words in the job&apos;s document
        /// </summary>
        public int WordCount { get; set; }
        /// <summary>
        /// The file extension of the job&apos;s document
        /// </summary>
        public string FileExtension { get; set; } = null!;
        /// <summary>
        /// The URL of the job&apos;s document got uploaded to online storage
        /// </summary>
        public string DocumentUrl { get; set; } = null!;
        /// <summary>
        /// The number of completed segment out of all segment
        /// </summary>
        public double Progress { get; set; }
        /// <summary>
        /// The project the this job is under
        /// </summary>
        public Guid ProjectId { get; set; }
        public string TargetLanguageId { get; set; } = null!;

        public virtual Project Project { get; set; } = null!;
        public virtual Language TargetLanguage { get; set; } = null!;
        public virtual ICollection<UserJob> UserJobs { get; set; }
    }
}
