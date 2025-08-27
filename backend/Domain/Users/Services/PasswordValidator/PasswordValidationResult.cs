namespace ExpenseTracker.Domain.Users.Services.PasswordValidator;

public class PasswordValidationResult
{
    public PasswordValidationResult()
    {
    }

    public PasswordValidationResult(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    public string? ErrorMessage { get; }

    public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);
}
