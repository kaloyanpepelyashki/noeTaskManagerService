using noeTaskManagerService.Services;
using System.ComponentModel.DataAnnotations;

namespace noeTaskManagerService.Models.Auth
{
    public class SignUpObject(string firstName, string lastName, string email, string password)
    {
           [Required]
           public string FirstName { get; set; } = firstName;
           [Required]
           public string LastName { get; set; } = lastName;
           [Required]
           public string Email { get; set; } = email;
           [Required]
           public string Password { get; set; } = PasswordEncryptor.HashPassword(password); //Hashes the password when initialising the object
    }
}
