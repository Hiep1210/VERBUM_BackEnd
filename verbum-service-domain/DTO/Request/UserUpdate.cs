using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using verbum_service_domain.DTO.Response;

namespace verbum_service_domain.DTO.Request
{
    public class UserUpdate
    {
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
        public UserInfo Data { get; set; }
    }
}
