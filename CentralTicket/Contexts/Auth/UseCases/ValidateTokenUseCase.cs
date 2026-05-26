using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CentralTicket.Contexts.Auth.UseCases
{
    public class ValidateTokenUseCase
    {
        private readonly IConfiguration _configuration;

        public ValidateTokenUseCase(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public ClaimsPrincipal? Run(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("AppSettings:Token")!);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            
            var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            
            if (principal == null) return null;

            return principal;
            
        }
    }
}
