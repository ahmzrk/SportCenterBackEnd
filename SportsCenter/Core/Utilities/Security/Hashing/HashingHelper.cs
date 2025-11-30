using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            Console.WriteLine($"[CREATE] Password: '{password}'");
            Console.WriteLine($"[CREATE] Length: {password?.Length}");
            Console.WriteLine($"[CREATE] Bytes: [{string.Join(",", Encoding.UTF8.GetBytes(password ?? ""))}]");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                Console.WriteLine($"[CREATE] Hash: {Convert.ToBase64String(passwordHash)}");
                Console.WriteLine($"[CREATE] Salt: {Convert.ToBase64String(passwordSalt)}");
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            Console.WriteLine($"[VERIFY] Password: '{password}'");
            Console.WriteLine($"[VERIFY] Length: {password?.Length}");
            Console.WriteLine($"[VERIFY] Bytes: [{string.Join(",", Encoding.UTF8.GetBytes(password ?? ""))}]");
            Console.WriteLine($"[VERIFY] Expected Hash: {Convert.ToBase64String(passwordHash)}");
            Console.WriteLine($"[VERIFY] Salt: {Convert.ToBase64String(passwordSalt)}");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                Console.WriteLine($"[VERIFY] Computed Hash: {Convert.ToBase64String(computedHash)}");

                bool isMatch = computedHash.SequenceEqual(passwordHash);
                Console.WriteLine($"[VERIFY] Match: {isMatch}");

                return isMatch;
            }
        }
    }
}
