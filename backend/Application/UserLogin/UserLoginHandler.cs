using ExpenseTracker.Application.Services.Auth;
using ExpenseTracker.Application.Services.PasswordEncryptor;
using ExpenseTracker.Domain.Users;
using MediatR;

namespace ExpenseTracker.Application.UserLogin;

public class UserLoginHandler : IRequestHandler<UserLoginRequest, UserLoginResponse>
    {
        private readonly IUserRepository userRepository;
        private readonly IPasswordEncryptor passwordEncryptor;
        private readonly IAuth auth;

        public UserLoginHandler(
            IUserRepository userRepository,
            IPasswordEncryptor passwordEncryptor,
            IAuth auth)
        {
            this.userRepository = userRepository;
            this.passwordEncryptor = passwordEncryptor;
            this.auth = auth;
        }

        public async Task<UserLoginResponse> Handle(UserLoginRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByEmail(request.Email);

            if (user is null)
            {
                return new UserLoginResponse
                {
                    Success = false,
                };
            }

            if (!passwordEncryptor.VerifyPassword(request.Password, user.PasswordSalt, user.PasswordHash))
            {
                return new UserLoginResponse
                {
                    Success = false,
                };
            }

            var token = auth.GenerateToken(new AuthenticatedUser(
                user.Id,
                user.Email.Address,
                user.EmailVerifiedAt is not null));

            // TODO: register last login date => make events? How to manage the transaction?

            return new UserLoginResponse
            {
                Success = true,
                Token = token.Token,
                ExpireDate = token.ExpireDate,
            };
        }
    }
