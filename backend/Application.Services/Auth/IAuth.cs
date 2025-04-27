namespace ExpenseTracker.Application.Services.Auth
{
    /// <summary>
    ///     Interface for the authentication service
    /// </summary>
    public interface IAuth
    {
        /// <summary>
        ///     Generate the token for the authenticated user
        /// </summary>
        /// <param name="authenticatedUser"></param>
        /// <returns></returns>
        AuthToken GenerateToken(AuthenticatedUser authenticatedUser);
    }
}
