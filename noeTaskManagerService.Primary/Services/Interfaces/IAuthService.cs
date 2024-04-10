using noeTaskManagerService.Models;

namespace noeTaskManagerService.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<User?> SignIn(string email, string hashedPassword);
        public Task<User?> SignUp(string firstName, string lastName, string email, string password);
    }
}
