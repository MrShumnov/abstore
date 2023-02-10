using Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Service.IService;

namespace Chamomile.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IUsersService _usersRepository;

        public AuthenticationController(ILogger<AuthenticationController> logger, 
            IConfiguration configuration, IUsersService userRepository)
        {
            _logger = logger;
            _configuration = configuration;
            _usersRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost("")]
        public async Task<IResult> Post(string userName, string password)
        {
            var role = await _usersRepository.CheckUser(userName, password);

            if (role != null)
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Role, role.Value.ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, userName),
                        new Claim(JwtRegisteredClaimNames.Email, password),
                     }),

                    Expires = DateTime.UtcNow.AddMinutes(5),
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"],
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"])),
                        SecurityAlgorithms.HmacSha512Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var stringToken = tokenHandler.WriteToken(token);

                _logger.LogInformation("User authorized");

                return Results.Ok(stringToken);
            }
            else
                return Results.Unauthorized();
        }
    }
}