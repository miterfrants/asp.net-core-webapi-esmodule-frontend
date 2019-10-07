using System;
using System.Security.Cryptography;
using System.Text;

using Sample.DTOs;

namespace Sample.Helpers
{
    public class CryptographicHelper
    {
        public static string GenerateSaltedHash(string password, string salt)
        {
            byte[] byteOfSalt = Encoding.ASCII.GetBytes(salt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, byteOfSalt, 10000);
            var hashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(128));
            return hashPassword;
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

        public static string GetSalt(int maxSize)
        {
            var salt = new byte[maxSize];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }
            return System.Text.Encoding.UTF8.GetString(salt);
        }
    }
}