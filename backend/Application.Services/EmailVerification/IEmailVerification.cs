namespace ExpenseTracker.Application.Services.EmailVerification
{
    /// <summary>
    ///   Interface for email verification service
    /// </summary>
    public interface IEmailVerification
    {
        /// <summary>
        ///       Generate a unique URL for email verification
        /// </summary>
        /// <returns>Unique URL</returns>
        string GenerateUniqueUrl();
    }
}
