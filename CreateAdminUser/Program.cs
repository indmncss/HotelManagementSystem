using System;
using System.Security.Cryptography;

namespace CreateAdminUser
{
    class Program
    {
        static void Main(string[] args)
        {
            string username = "admin";
            string password = "Admin@123"; // default password
            string fullName = "Administrator";
            string email = "admin@example.com";
            int roleId = 1; // Admin
            int iterations = 100000;

            // Generate salt (16 bytes)
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Derive hash (32 bytes)
            byte[] hash;
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                hash = pbkdf2.GetBytes(32);
            }

            string hashBase64 = Convert.ToBase64String(hash);
            string saltBase64 = Convert.ToBase64String(salt);

            string sql = $@"
-- INSERT ADMIN USER
INSERT INTO Users (Username, PasswordHash, PasswordSalt, PasswordIterations, FullName, Email, RoleId, IsActive, CreatedAt)
VALUES (N'{username}', N'{hashBase64}', N'{saltBase64}', {iterations}, N'{fullName}', N'{email}', {roleId}, 1, SYSUTCDATETIME());
";

            Console.WriteLine("----- COPY THE SQL BELOW -----");
            Console.WriteLine(sql);
            Console.WriteLine("----- END SQL -----");

            Console.WriteLine("\nDefault password: " + password);
        }
    }
}
