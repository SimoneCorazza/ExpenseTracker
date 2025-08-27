namespace ExpenseTracker.Application.Services.PasswordEncryptor;

public interface IPasswordEncryptor
{
    byte[] GenerateSalt();

    byte[] HashPassword(string password, byte[] salt);

    bool VerifyPassword(string password, byte[] salt, byte[] hash);
}
