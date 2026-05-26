using CentralTicket.Contexts.Auth.Data;
using CentralTicket.Contexts.Auth.Entities;

namespace CentralTicket.Contexts.Auth.UseCases
{
    public class GenerateAndSaveRefreshTokenUseCase
    {
        private readonly GenerateRefreshTokenUseCase _generateRefreshTokenUseCase;

        private readonly Context _context;
        public GenerateAndSaveRefreshTokenUseCase(GenerateRefreshTokenUseCase generateRefreshTokenUseCase, Context context)
        {
            _generateRefreshTokenUseCase = generateRefreshTokenUseCase;
            _context = context;
        }

        public async Task<string> Run(User user)
        {
            var refreshToken = _generateRefreshTokenUseCase.Run();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _context.SaveChangesAsync();

            return refreshToken;    
        }
    }
}
