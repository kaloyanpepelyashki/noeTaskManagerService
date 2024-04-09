using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using noeTaskManagerService.Models;
using noeTaskManagerService.Models.Auth;
using noeTaskManagerService.Services;
using System.ComponentModel.DataAnnotations;


namespace noeTaskManagerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        protected readonly AuthService _authService;
        protected readonly JWTGenerator _jwtGenerator;

        public AuthController()
        {
            _authService = AuthService.GetInstance();
            _jwtGenerator = new JWTGenerator();
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

                var user = new SuccessReturnObject(result.FirstName, result.LastName, result.Email, result.UserUKey);
                var jwt = _jwtGenerator.GenerateJWTToken(userCred);

                return Ok(new { access_token = jwt, user_cred = user });

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

                var userObject = new SuccessReturnObject(result.FirstName, result.LastName, result.Email, result.UserUKey);
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
    }
}
