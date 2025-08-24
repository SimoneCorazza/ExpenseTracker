using MediatR;

namespace ExpenseTracker.Application.UserLogin;

public class UserLoginRequest : IRequest<UserLoginResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
