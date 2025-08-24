namespace ExpenseTracker.Domain.Users.Services.PasswordValidator;

/// <summary>
    ///     Service to validate passwords
    /// </summary>
    public interface IPasswordValidator
    {
        /// <summary>
        ///     Validate the password
        /// </summary>
        /// <param name="password">Password to evaluate</param>
        /// <returns>Validation result</returns>
        PasswordValidationResult Validate(string password);
    }
