using ExpenseTracker.Application.Services.EmailVerification;
using ExpenseTracker.Application.Services.PasswordEncryptor;
using ExpenseTracker.Domain;
using ExpenseTracker.Domain.Users;
using ExpenseTracker.Domain.Users.Services.PasswordValidator;
using MediatR;

namespace ExpenseTracker.Application.Users.Registration;

public class UserRegistrationHandler : IRequestHandler<RegisterRequest, UserRegistrationResponse>
    {
        private readonly IUserRepository userRepository;
        private readonly IPasswordValidator passwordValidator;
        private readonly IPasswordEncryptor passwordEncryptor;
        private readonly IEmailVerification emailVerification;

        public UserRegistrationHandler(
            IUserRepository userRepository,
            IPasswordValidator passwordValidator,
            IPasswordEncryptor passwordEncryptor,
            IEmailVerification emailVerification)
        {
            this.userRepository = userRepository;
            this.passwordValidator = passwordValidator;
            this.passwordEncryptor = passwordEncryptor;
            this.emailVerification = emailVerification;
        }

        public async Task<UserRegistrationResponse> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            if (await userRepository.ExistWithEmail(request.Email))
            {
                return new UserRegistrationResponse
                {
                    ErrorCode = ErrorCode.EmailAlreadyInUse,
                };
            }

            if (!passwordValidator.Validate(request.Password).IsSuccess)
            {
                return new UserRegistrationResponse
                {
                    ErrorCode = ErrorCode.InvalidPassword,
                };
            }


            var salt = passwordEncryptor.GenerateSalt();
            var passwordHash = passwordEncryptor.HashPassword(request.Password, salt);
            var verificationBlob = emailVerification.GenerateUniqueUrl();

            try
            {
                var user = new User(
                    new EmailAddress(request.Email),
                    verificationBlob,
                    passwordHash,
                    salt);

                // Does not need explicit transaction correct?
                userRepository.Add(user);

                return new UserRegistrationResponse
                {
                    UserId = user.Id,
                    ErrorCode = null,
                };
            }
            catch (DomainException ex)
            {
                return new UserRegistrationResponse
                {
                    ErrorCode = ErrorCode.InvalidEmail,
                };
            }
        }
    }
