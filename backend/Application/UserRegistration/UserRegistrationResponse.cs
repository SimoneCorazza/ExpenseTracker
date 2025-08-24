namespace ExpenseTracker.Application.UserRegistration;

public class UserRegistrationResponse
    {
        /// <summary>
        ///     The unique identifier for the user.
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        ///     Error code; null if everything was ok
        /// </summary>
        public ErrorCode? ErrorCode { get; set; }
    }
