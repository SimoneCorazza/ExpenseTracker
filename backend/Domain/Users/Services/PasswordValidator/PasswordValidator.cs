namespace ExpenseTracker.Domain.Users.Services.PasswordValidator;

/// <inheritdoc cref="IPasswordValidator" />
    public class PasswordValidator : IPasswordValidator
    {
        private readonly int minPasswordLength;
        private readonly string specialCharachters;

        public PasswordValidator(int minPasswordLength, string specialCharachters)
        {
            if (minPasswordLength < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minPasswordLength), "Minimum password length must be at least 0 characters.");
            }

            if (string.IsNullOrWhiteSpace(specialCharachters))
            {
                throw new ArgumentException("Special characters cannot be null or empty.", nameof(specialCharachters));
            }

            this.minPasswordLength = minPasswordLength;
            this.specialCharachters = specialCharachters;
        }

        /// <inheritdoc />
        public PasswordValidationResult Validate(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return new PasswordValidationResult("Password cannot be empty or whitespace.");
            }

            if (password.Length < minPasswordLength)
            {
                return new PasswordValidationResult("Password must be at least 8 characters long.");
            }

            if (!password.Any(char.IsUpper))
            {
                return new PasswordValidationResult("Password must contain at least one uppercase letter.");
            }

            if (!password.Any(char.IsLower))
            {
                return new PasswordValidationResult("Password must contain at least one lowercase letter.");
            }

            if (!password.Any(char.IsDigit))
            {
                return new PasswordValidationResult("Password must contain at least one digit.");
            }

            if (!password.Any(specialCharachters.Contains))
            {
                return new PasswordValidationResult("Password must contain at least one special character.");
            }

            return new PasswordValidationResult();
        }
    }
