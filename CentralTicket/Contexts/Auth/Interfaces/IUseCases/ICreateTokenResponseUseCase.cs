using CentralTicket.Contexts.Auth.Dtos;
using CentralTicket.Contexts.Auth.Entities;
using CentralTicket.Contexts.Billing.DTOs.Sale;

namespace CentralTicket.Contexts.Auth.Interfaces.IUseCases
{
    public interface ICreateTokenResponseUseCase
    {
        public Task<TokenResponseDto> Run(User user);
    }
}
