namespace ExpenseTracker.Application.Services.Auth;

public class AuthenticatedUser
{
    public AuthenticatedUser(Guid userId, string email, bool verifiedEmail)
    {
        UserId = userId;
        Email = email;
        VerifiedEmail = verifiedEmail;
    }

    /// <summary>
    ///     User's unique identifier
    /// </summary>
    public Guid UserId { get; }

    /// <summary>
    ///    User's email address
    /// </summary>
    public string Email { get; }

    /// <summary>
    ///     True if the email is verified, false otherwise
    /// </summary>
    public bool VerifiedEmail { get; }
}
