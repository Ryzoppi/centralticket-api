using CentralTicket.Contexts.Auth.Dtos;
using CentralTicket.Contexts.Auth.Requests;

namespace CentralTicket.Contexts.Auth.Interfaces.IUseCases
{
    public interface IRefreshTokensUseCase
    {
        public Task<TokenResponseDto?> Run(RefreshTokenRequest request);
    }
}
