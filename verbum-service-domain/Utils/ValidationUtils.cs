using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace verbum_service_domain.Utils
{
    public class ValidationUtils
    {
        public static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsValidPassword(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@!#$%^&*]).{8,12}$";
            Regex regex = new Regex(pattern);
            return ObjectUtils.IsNotEmpty(password) && regex.IsMatch(password);
        }
    }
}
