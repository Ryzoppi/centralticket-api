using CentralTicket.Contexts.Auth.Dtos;
using CentralTicket.Contexts.Auth.Requests;

namespace CentralTicket.Contexts.Auth.Interfaces.IUseCases
{
    public interface ILoginUseCase
    {
        public Task<TokenResponseDto?> Run(LoginRequest request);
    }
}
