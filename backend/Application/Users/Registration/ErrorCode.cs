namespace ExpenseTracker.Application.Users.Registration;

/// <summary>
    ///     Error during the user registration
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        ///       The email is already in use
        /// </summary>
        EmailAlreadyInUse = 1,

        /// <summary>
        ///       The email is not valid
        /// </summary>
        InvalidEmail = 2,

        /// <summary>
        ///      The password is not valid
        /// </summary>
        InvalidPassword = 3,
    }
