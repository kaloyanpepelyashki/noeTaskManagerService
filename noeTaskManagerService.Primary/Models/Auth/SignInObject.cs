using System.ComponentModel.DataAnnotations;

namespace noeTaskManagerService.Models.Auth
{
    public class SignInObject(string email, string password)
    {
           [Required]
           public string Email { get; set; } = email;
           [Required]
           public string Password { get; set; } = password;
    }
}
