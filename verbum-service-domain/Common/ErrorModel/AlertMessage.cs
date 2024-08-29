using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using verbum_service_domain.Models.Results;

namespace verbum_service_domain.Common.ErrorModel
{
    public class AlertMessage
    {
        public static string Alert(string alertCode, params string[] parameter)
        {
            return string.Format(alertCode, parameter);
        }
    }
}
