using auth_hexagonal_arch_module.Domain.Entities;
using auth_hexagonal_arch_module.Domain.Repository;
using auth_hexagonal_arch_module.Domain.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace auth_hexagonal_arch_module.Infrastructure.Services
{
    public class AuthJwtService : IAuthService
    {
        private readonly IUserRepository _userPostgresRepository;
        private readonly IConfiguration _configuration;

        public AuthJwtService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userPostgresRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<Auth?> Login(User user)
        {
            User existingUser = await _userPostgresRepository.GetById(user.Id);

            if (existingUser == null || !BCrypt.Net.BCrypt.Verify(user.Password, existingUser.Password))
            {
                return null;
            }

            var token = GenerateJwtToken(existingUser);

            return new Auth
            {
                Token = token,
                Name = existingUser.Name
            };
        }

        public string GenerateJwtToken(User user)
        {
            var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET");
            var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
            var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
            var expirationMinutes = Environment.GetEnvironmentVariable("JWT_EXPIRATION_MINUTES");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(expirationMinutes)),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
