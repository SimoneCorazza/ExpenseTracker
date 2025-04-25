namespace ExpenseTracker.Application.Services.Auth
{
    
    public class AuthenticatedUser
    {
        /// <summary>
        ///     User's unique identifier
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        ///    User's email address
        /// </summary>
        public string Email { get; set; }


        /// <summary>
        ///     True if the email is verified, false otherwise
        /// </summary>
        public bool VerifiedEmail { get; set; }
    }
}
