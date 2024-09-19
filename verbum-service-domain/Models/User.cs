using System;
using System.Collections.Generic;

namespace verbum_service_domain.Models
{
    public partial class User
    {
        public User()
        {
            Revelancies = new HashSet<Revelancy>();
            UserCompanies = new HashSet<UserCompany>();
            UserJobs = new HashSet<UserJob>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime? EmailVerified { get; set; }
        public int? ImageId { get; set; }
        public string? Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Status { get; set; } = null!;
        public int? TokenId { get; set; }

        public virtual Image? Image { get; set; }
        public virtual RefreshToken? Token { get; set; }
        public virtual ICollection<Revelancy> Revelancies { get; set; }
        public virtual ICollection<UserCompany> UserCompanies { get; set; }
        public virtual ICollection<UserJob> UserJobs { get; set; }
    }
}
