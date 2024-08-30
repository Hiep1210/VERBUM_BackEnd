using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace verbum_service_domain.Common.ErrorModel
{
    public class ValidationAlertCode
    {
        public const string REQUIRED = "{0} is required";
        public const string FAILED_VALIDATION = "{0} failed validation";
        public const string LENGTH_RANGE_FAILED = "{0} length is from {1} to {2}";
        public const string NOT_FOUND = "{0} is not found in database";
        public const string DUPLICATE = "{0} is already in database";
        public const string INVALID = "{0} is invalid";
        public const string EMAIL_NOT_VERIFIED = "Email is not verified";
    }
}
