using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libreriaClases
{
    public class Hash
    {
        public static string GetHash(string password)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt(8);
            var hash = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return hash;
        }

        public static bool ValidatePassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
