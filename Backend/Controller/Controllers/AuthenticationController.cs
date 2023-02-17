using Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Service.IService;
using Service.Dto;

namespace Chamomile.Controllers
{
    [ApiController]
    [Route("api/auth")]
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
        [HttpPost]
        public async Task<IResult> Post(AuthDto dto)
        {
            var user = await _usersRepository.CheckUser(dto.Login, dto.Password);

            if (user != null)
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Role, user.Role.ToString()),
                        new Claim("UserId", user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, dto.Login),
                        new Claim(JwtRegisteredClaimNames.Email, dto.Password),
                     }),

                    Expires = DateTime.UtcNow.AddMinutes(10),
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