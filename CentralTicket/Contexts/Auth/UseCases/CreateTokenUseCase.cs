using CentralTicket.Contexts.Auth.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CentralTicket.Contexts.Auth.UseCases
{
    public class CreateTokenUseCase
    {
        private readonly IConfiguration _configuration;

        public CreateTokenUseCase(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public string Run(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name.Value)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
