using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using verbum_service_domain.Models;

namespace verbum_service_domain.DTO.Request
{
    public class UserSignUp
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Password { get; set; }
        public string RoleName { get; set; } = null!;
        public bool IsVerified { get; set; } = false;
    }
}
