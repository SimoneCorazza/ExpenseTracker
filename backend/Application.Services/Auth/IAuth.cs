namespace ExpenseTracker.Application.Services.Auth
{
    public interface IAuth
    {
        AuthToken GenerateToken(AuthenticatedUser authenticatedUser);

        AuthenticatedUser ParseToken(string token);
    }
}
