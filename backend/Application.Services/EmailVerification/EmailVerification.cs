using System.Security.Cryptography;

namespace ExpenseTracker.Application.Services.EmailVerification;

public class EmailVerification : IEmailVerification
    {
        private readonly int size;

        public EmailVerification(int size)
        {
            this.size = size;
        }

        public string GenerateUniqueUrl()
        {
            byte[] data = new byte[size];

            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(data);

            return Convert.ToBase64String(data)
                .Replace('/', '-') // Remove slash that it generates problems in URLs
                .TrimEnd('='); // Remove padding
        }
    }
