namespace ExpenseTracker.Domain.Users
{
    public class User
    {
        /// <summary>
        ///     User ID
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        ///     Email address for the user
        /// </summary>
        public EmailAddress Email { get; private set; }

        /// <summary>
        ///     Password hash
        /// </summary>
        public byte[] PasswordHash { get; private set; }

        /// <summary>
        ///     Password salt
        /// </summary>
        public byte[] PasswordSalt { get; private set; }

        /// <summary>
        ///   User last login date
        /// </summary>
        public DateTime LastLogIn { get; private set; }

        /// <summary>
        ///   User creation date
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        ///    Date when the user email was verified
        /// </summary>
        public DateTime? EmailVerifiedAt { get; private set; }

        /// <summary>
        ///   Email verification blob
        /// </summary>
        public string EmailVerificationBlob { get; private set; }

        public User(EmailAddress email, string verificationBlob, byte[] passwordHash, byte[] passwordSalt)
        {
            Id = Guid.NewGuid();
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            EmailVerificationBlob = verificationBlob;

            CreatedAt = DateTime.UtcNow;
            LastLogIn = CreatedAt;

            Validate();
        }

        protected User()
        {
        }

        public void UpdateLastLogin()
        {
            LastLogIn = DateTime.UtcNow;

            Validate();
        }

        public void UpdateEmail(EmailAddress email, string verificationBlob)
        {
            Email = email;
            EmailVerifiedAt = null;
            EmailVerificationBlob = verificationBlob;

            Validate();
        }

        public void EmailVerified()
        {
            if (EmailVerifiedAt is not null)
            {
                throw new DomainException("Email already verified");
            }

            EmailVerifiedAt = DateTime.UtcNow;

            Validate();
        }

        public void UpdatePassword(byte[] passwordHash, byte[] passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;

            Validate();
        }

        private void Validate()
        {
            if (Email is null)
            {
                throw new DomainException("Email is mandatory");
            }

            if (PasswordHash is null || PasswordHash.Length == 0)
            {
                throw new DomainException("Password hash is mandator");
            }

            if (PasswordSalt is null || PasswordSalt.Length == 0)
            {
                throw new DomainException("Password salt is mandator");
            }

            if (string.IsNullOrWhiteSpace(EmailVerificationBlob))
            {
                throw new DomainException("Email verification blob is mandator");
            }

            if (LastLogIn > CreatedAt)
            {
                throw new DomainException("The login cannot happen before the creation");
            }
        }
    }
}
