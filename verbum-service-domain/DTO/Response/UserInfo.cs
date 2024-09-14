using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace verbum_service_domain.DTO.Response
{
    public class UserInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Relevancy { get; set; }
        public string UserCompanyStatus { get; set; }
    }
}
