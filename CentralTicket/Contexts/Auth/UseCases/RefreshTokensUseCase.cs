using CentralTicket.Contexts.Auth.Dtos;
using CentralTicket.Contexts.Auth.Interfaces.IUseCases;
using CentralTicket.Contexts.Auth.Requests;

namespace CentralTicket.Contexts.Auth.UseCases
{
    public class RefreshTokensUseCase : IRefreshTokensUseCase
    {
        private readonly ValidateRefreshTokenUseCase _validateRefreshToken;
        private readonly CreateTokenResponseUseCase _createTokenResponseUseCase;

        public RefreshTokensUseCase(ValidateRefreshTokenUseCase validateRefreshToken, CreateTokenResponseUseCase createTokenResponseUseCase)
        {
            _validateRefreshToken = validateRefreshToken;
            _createTokenResponseUseCase = createTokenResponseUseCase;
        }

        public async Task<TokenResponseDto?> Run(RefreshTokenRequest request)
        {
            var user = await _validateRefreshToken.Run(request.UserId, request.RefreshToken);
            if (user == null)
            {
                return null;
            }

            TokenResponseDto tokenResponse = await _createTokenResponseUseCase.Run(user);

            return tokenResponse;
        }
    }
}
