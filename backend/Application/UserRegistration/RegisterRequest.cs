using MediatR;

namespace ExpenseTracker.Application.UserRegistration;

public class RegisterRequest : IRequest<UserRegistrationResponse>
    {
        /// <summary>
        ///     Email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///    Password
        /// </summary>
        public string Password { get; set; }
    }
