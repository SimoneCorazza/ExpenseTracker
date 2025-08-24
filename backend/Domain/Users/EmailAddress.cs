using System.Net.Mail;

namespace ExpenseTracker.Domain.Users;

/// <summary>
    ///   Represents an email address
    /// </summary>
    public class EmailAddress
    {
        public string Address { get; private set; }

        /// <summary>
        ///    Constructor for creating an email address
        /// </summary>
        /// <param name="address">Email address</param>
        /// <exception cref="ArgumentException"></exception>
        public EmailAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new DomainException("Email address cannot be null or empty.");
            }

            if (!MailAddress.TryCreate(address, out var _))
            {
                throw new DomainException("Invalid email address format.");
            }

            Address = address;
        }
    }
