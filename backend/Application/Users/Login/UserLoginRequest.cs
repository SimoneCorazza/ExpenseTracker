using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Application.Users.Login;

/// <summary>
///     Request to log in
/// </summary>
public class UserLoginRequest : IRequest<UserLoginResponse>
{
    /// <summary>
    ///     Email
    /// </summary>
    [Required]
    public string Email { get; set; }

    /// <summary>
    ///     Password
    /// </summary>
    [Required]
    public string Password { get; set; }
}
