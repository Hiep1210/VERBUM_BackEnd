using System.Security.Cryptography;
using System.Text;

namespace verbum_service_domain.Utils
{
    public class UserUtils
    {
        //input password return undecryptable hash, validate by hash input and compare to the db
        public static string HashPassword(string input)
        {
            //Convert the input to a byte array using specified encoding
            var InputBuffer = Encoding.Unicode.GetBytes(input);
            //Hash the input
            byte[] HashedBytes;
            using (var Hasher = new SHA256Managed())
            {
                HashedBytes = Hasher.ComputeHash(InputBuffer);
            }
            //Return
            return BitConverter.ToString(HashedBytes).Replace("-", string.Empty);
        }
    }
}
