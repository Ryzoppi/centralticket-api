using CentralTicket.Contexts.Auth.Data;
using CentralTicket.Contexts.Auth.Entities;
using CentralTicket.Contexts.Auth.Interfaces.IRepositories;
using CentralTicket.Contexts.Auth.Interfaces.IUseCases;

namespace CentralTicket.Contexts.Auth.UseCases
{
    public class GenerateAndSaveRefreshTokenUseCase : IGenerateAndSaveRefreshTokenUseCase
    {
        private readonly GenerateRefreshTokenUseCase _generateRefreshTokenUseCase;

        private readonly IUserRepository _userRepository;
        public GenerateAndSaveRefreshTokenUseCase(GenerateRefreshTokenUseCase generateRefreshTokenUseCase, IUserRepository userRepository)
        {
            _generateRefreshTokenUseCase = generateRefreshTokenUseCase;
            _userRepository = userRepository;
        }

        public async Task<string> Run(User user)
        {
            var refreshToken = _generateRefreshTokenUseCase.Run();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            _userRepository.SaveChanges();

            return refreshToken;    
        }
    }
}
