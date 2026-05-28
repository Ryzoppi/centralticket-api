using CentralTicket.Contexts.Auth.Entities;

namespace CentralTicket.Contexts.Auth.Interfaces.IUseCases
{
    public interface ICreateTokenUseCase
    {
        public string Run(User user);
    }
}
