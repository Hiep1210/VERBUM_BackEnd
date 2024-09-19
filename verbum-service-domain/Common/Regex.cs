using System.Text.RegularExpressions;

namespace verbum_service_domain.Common
{
    public class Regex
    {
        public static readonly System.Text.RegularExpressions.Regex ALPHA_NUMERIC = new("^[a-zA-Z0-9]*$");
        public static readonly System.Text.RegularExpressions.Regex ALPHA = new("^[a-zA-Z]*$");
        public static readonly System.Text.RegularExpressions.Regex NUMERIC = new("^[0-9]*$");
    }
}
