using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using noeTaskManagerService.Services;


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
                var result = await _authService.SignIn(userCred.Email, userCred.Password);


                if(result == null)
                {
                    return BadRequest("No user with this credentials");
                }

                return Ok(result);

            } catch(AuthException e)
            {
                return StatusCode(400, e.Message);

            } catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }




        public class SignInObject(string email, string password)
        {
            public string Email { get; set; } = email;
            public string Password { get; set; } = password;
        }
    }
}
