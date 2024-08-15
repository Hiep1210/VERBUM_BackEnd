using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using verbum_service_domain.Models;

namespace verbum_service_application.Validation
{
    internal interface IValidation<T>
    {
        public List<AlertMessage> Validate(T entity);
    }
}
