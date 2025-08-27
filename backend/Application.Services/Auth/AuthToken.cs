namespace ExpenseTracker.Application.Services.Auth;

public class AuthToken
{
    public string Token { get; init; }
    public DateTime ExpireDate { get; init; }
}
