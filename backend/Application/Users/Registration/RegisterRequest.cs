using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Application.Users.Registration;

/// <summary>
///     Request to register a new user
/// </summary>
public class RegisterRequest : IRequest<UserRegistrationResponse>
{
    /// <summary>
    ///     Email address
    /// </summary>
    [Required]
    public string Email { get; set; }

    /// <summary>
    ///    Password
    /// </summary>
    [Required]
    public string Password { get; set; }
}
