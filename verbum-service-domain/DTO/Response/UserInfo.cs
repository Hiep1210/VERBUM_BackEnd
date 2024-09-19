using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace verbum_service_domain.DTO.Response
{
    public class UserInfo
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Relevancy { get; set; }
        public string CreatedAt { get; set; }
        public string UserCompanyStatus { get; set; }
    }
}
