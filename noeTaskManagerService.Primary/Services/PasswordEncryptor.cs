using BCr = BCrypt.Net;
namespace noeTaskManagerService.Services
{
    public class PasswordEncryptor
    {
        public static string HashPassword(string password)
        {
            if(String.IsNullOrWhiteSpace(password))
            {
                throw new PasswordEncryptorException("No password was passed to the encryptor");
            }
            return BCr.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCr.BCrypt.Verify(password, hashedPassword);
        }
    }

    public class PasswordEncryptorException: Exception
    {
        public PasswordEncryptorException(string message) : base(message)
        {

        }
    }
}
