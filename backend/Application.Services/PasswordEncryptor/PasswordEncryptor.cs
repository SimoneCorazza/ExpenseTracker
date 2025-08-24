using System.Security.Cryptography;
using System.Text;

namespace ExpenseTracker.Application.Services.PasswordEncryptor;

public class PasswordEncryptor : IPasswordEncryptor
    {
        private readonly int saltSize;

        public PasswordEncryptor(int saltSize)
        {
            this.saltSize = saltSize;
        }

        public byte[] GenerateSalt()
        {
            var salt = new byte[saltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public byte[] HashPassword(string password, byte[] salt)
        {
            using var hmac = new HMACSHA256(salt);
            return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public bool VerifyPassword(string password, byte[] salt, byte[] hash)
        {
            var computedHash = HashPassword(password, salt);
            return computedHash.SequenceEqual(hash);
        }
    }
