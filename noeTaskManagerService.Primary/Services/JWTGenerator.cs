using Microsoft.IdentityModel.Tokens;
using noeTaskManagerService.Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace noeTaskManagerService.Services
{
    public class JWTGenerator
    {
        protected IConfiguration _configuration;

        protected string JwtIssuer;
        protected string JwtAudience;
        protected string JwtSecurityKey;

        public JWTGenerator() 
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();
            JwtIssuer = _configuration.GetValue<string>("JWT:Issuer");
            JwtAudience = _configuration.GetValue<string>("JWT:Audience");
            JwtSecurityKey = _configuration.GetValue<string>("JWT:SecretKey");
        }

        public string GenerateJWTToken(SignInObject userDto)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecurityKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                List<Claim> claims =
                [
                    new Claim(JwtRegisteredClaimNames.Sub, userDto.Email),
                    new Claim("email", userDto.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                ];

                var token = new JwtSecurityToken(
                    issuer: JwtIssuer,
                    audience: JwtAudience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpiryMinutes"])),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error generating token {e}");
                return null;
            }
        }
    }
}
