using MediatR;

namespace ExpenseTracker.Application.Users.Login;

public class UserLoginRequest : IRequest<UserLoginResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
