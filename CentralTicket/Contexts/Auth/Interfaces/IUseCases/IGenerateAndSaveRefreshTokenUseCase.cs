using CentralTicket.Contexts.Auth.Entities;

namespace CentralTicket.Contexts.Auth.Interfaces.IUseCases
{
    public interface IGenerateAndSaveRefreshTokenUseCase
    {
        public Task<string> Run(User user);
    }
}
