using CentralTicket.Contexts.Auth.Entities;
using CentralTicket.Contexts.Auth.Requests;

namespace CentralTicket.Contexts.Auth.Interfaces.IUseCases
{
    public interface IRegisterUseCase
    {
        public User? Run(RegisterRequest request);
    }
}
