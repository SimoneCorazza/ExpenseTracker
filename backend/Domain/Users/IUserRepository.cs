namespace ExpenseTracker.Domain.Users;

public interface IUserRepository : IRepository<User>
{
    /// <summary>
    ///     Check if a user exists with the given email
    /// </summary>
    /// <param name="email">Email address</param>
    /// <returns>True if the user exists, false otherwise</returns>
    Task<bool> ExistWithEmail(string email);

    /// <summary>
    ///     Get a user by email
    /// </summary>
    /// <param name="email">Email address</param>
    /// <returns>User or null if not found</returns>
    Task<User?> GetByEmail(string email);
}
