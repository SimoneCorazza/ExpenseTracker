namespace ExpenseTracker.Application.UserLogin
{
    public class UserLoginResponse
    {
        /// <summary>
        ///     True if the login was successfull, false otherwise
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        ///     Token
        /// </summary>
        public string? Token { get; set; }

        /// <summary>
        ///     Expire date of the token
        /// </summary>
        public DateTime? ExpireDate { get; set; }
    }
}
