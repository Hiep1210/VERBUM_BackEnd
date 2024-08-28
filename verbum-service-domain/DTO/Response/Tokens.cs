using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace verbum_service_domain.DTO.Response
{
    public class Tokens
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
