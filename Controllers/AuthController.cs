using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using noeTaskManagerService.Models;
using noeTaskManagerService.Services;
using System.ComponentModel.DataAnnotations;


namespace noeTaskManagerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        protected readonly AuthService _authService;

        public AuthController()
        {
            _authService = AuthService.GetInstance();
        }

        [HttpPost("/signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInObject userCred)
        {
            try
            {
                 User result = await _authService.SignIn(userCred.Email, userCred.Password);


                if(result == null)
                {
                    return BadRequest("No user with this credentials");
                }

                var user = new UserReturnObject(result.FirstName, result.LastName, result.Email, result.UserUKey);
                return Ok(user);

            } catch(AuthException e)
            {
                return StatusCode(400, e.Message);

            } catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpPost("/signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpObject userCred)
        {
            try
            {
                User? result = await _authService.SignUp(userCred.FirstName, userCred.LastName, userCred.Email, userCred.Password);

                if(result == null)
                {
                    return BadRequest("Error creating account");
                }

                var userObject = new UserReturnObject(result.FirstName, result.LastName, result.Email, result.UserUKey);
                return Ok(userObject);
            }
            catch (AuthException e)
            {
                return StatusCode(400, e.Message);

            } catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }




        public class SignInObject(string email, string password)
        {
            [Required]
            public string Email { get; set; } = email;
            [Required]
            public string Password { get; set; } = password;
        }

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

        public class UserReturnObject(string firstName, string lastName, string email, string uuid)
        {
            public string FirstName { get; set;} = firstName;
            public string LastName { get; set;} = lastName;
            public string Email { get; set;} = email;
            public string UserUID { get; set;} = uuid;

        }
    }
}
