using System.Security.Claims;

namespace ExpenseTracker.Application.Services.Auth;

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

        /// <summary>
        ///     Convert from claims principal to authenticated user
        /// </summary>
        /// <param name="claimsPrincipal">Calims to convert</param>
        /// <returns>Authenticated user</returns>
        AuthenticatedUser FromClaims(ClaimsPrincipal claimsPrincipal);
    }
