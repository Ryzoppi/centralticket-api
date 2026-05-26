using CentralTicket.Contexts.Auth.Dtos;
using CentralTicket.Contexts.Auth.Entities;

namespace CentralTicket.Contexts.Auth.UseCases
{
    public class CreateTokenResponseUseCase
    {
        private readonly CreateTokenUseCase _createTokenUseCase;
        private readonly GenerateAndSaveRefreshTokenUseCase _generateAndSaveRefreshTokenUseCase;
        public CreateTokenResponseUseCase(CreateTokenUseCase createTokenUseCase, GenerateAndSaveRefreshTokenUseCase generateAndSaveRefreshTokenUseCase)
        {
            _createTokenUseCase = createTokenUseCase;
            _generateAndSaveRefreshTokenUseCase = generateAndSaveRefreshTokenUseCase;
        }
        public async Task<TokenResponseDto> Run(User user)
        {
            return new TokenResponseDto
            {
                AccessToken = _createTokenUseCase.Run(user),
                RefreshToken = await _generateAndSaveRefreshTokenUseCase.Run(user)
            };
        }
    }
}
