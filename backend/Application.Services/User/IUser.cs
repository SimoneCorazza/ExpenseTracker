using ExpenseTracker.Application.Services.Auth;

namespace ExpenseTracker.Application.Services.User;

/// <summary>
    ///     Provides the information of the logged user
    /// </summary>
    public interface IUser
    {
        /// <summary>
        ///     The logged user; null if none
        /// </summary>
        AuthenticatedUser? LoggedUser { get; }
    }
