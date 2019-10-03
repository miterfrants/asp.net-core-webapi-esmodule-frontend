using System;
using System.Security.Cryptography;
using System.Text;

namespace Sample.Helpers
{
    public class CryptographicHelper
    {
        public static SaltedPassword GenerateSaltedHash(int maxSize, string password)
        {
            byte[] salt = GetSalt(maxSize);
            string saltString = System.Text.Encoding.UTF8.GetString(salt);

            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, 10000);
            var hashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(128));

            SaltedPassword saltedPassword = new SaltedPassword { Hash = hashPassword, Salt = saltString };
            return saltedPassword;
        }

        public static string GetSpecificLengthRandomString(int size, bool isLowerCase = false)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (isLowerCase)
            {
                return builder.ToString().ToLower();
            }
            return builder.ToString();
        }

        private static byte[] GetSalt(int maxSize)
        {
            var salt = new byte[maxSize];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }
            return salt;
        }
    }

    public class SaltedPassword
    {
        public string Hash { get; set; }
        public string Salt { get; set; }
    }

    
}