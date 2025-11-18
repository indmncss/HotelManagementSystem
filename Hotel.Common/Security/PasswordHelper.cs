using System;
using System.Security.Cryptography;

namespace Hotel.Common.Security
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password, out string saltBase64, int iterations = 100000)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var salt = new byte[16];
                rng.GetBytes(salt);

                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
                {
                    var hash = pbkdf2.GetBytes(32); // 256-bit
                    saltBase64 = Convert.ToBase64String(salt);
                    return Convert.ToBase64String(hash);
                }
            }
        }

        public static bool VerifyPassword(string password, string storedHashBase64, string saltBase64, int iterations)
        {
            var salt = Convert.FromBase64String(saltBase64);
            var storedHash = Convert.FromBase64String(storedHashBase64);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                var computed = pbkdf2.GetBytes(32);
                return SlowEquals(computed, storedHash);
            }
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;

            int diff = 0;
            for (int i = 0; i < a.Length; i++)
            {
                diff |= a[i] ^ b[i]; // prevents timing attacks
            }
            return diff == 0;
        }
    }
}
