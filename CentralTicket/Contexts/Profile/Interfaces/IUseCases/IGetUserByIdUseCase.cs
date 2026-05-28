using CentralTicket.Contexts.Profile.DTOs.User;

namespace CentralTicket.Contexts.Profile.Interfaces.IUseCases
{
    public interface IGetUserByIdUseCase
    {
        public ReadUserDTO Run(Guid id);
    }
}
