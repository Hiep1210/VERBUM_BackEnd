using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace verbum_service_domain.Common.ErrorModel
{
    public class BusinessException : Exception
    {
        public List<string> Errors { get; set; }
        public BusinessException(List<string> messages)
        {
            Errors = messages;
        }
        public BusinessException(string message)
        {
            Errors = new List<string> { message };
        }
        public BusinessException() { }
    }
}
