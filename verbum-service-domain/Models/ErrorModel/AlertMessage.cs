using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace verbum_service_domain.Models.ErrorModel
{
    public class AlertMessage
    {
        public static string Alert(string alertCode, params string[] parameter)
        {
            return string.Format(alertCode, parameter);
        }
    }
}
