using BCr = BCrypt.Net;
namespace noeTaskManagerService.Services
{
    public class PasswordEncryptor
    {
        public static string HashPassword(string password)
        {
            return BCr.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCr.BCrypt.Verify(password, hashedPassword);
        }
    }
}
