using CentralTicket.Contexts.Profile.DTOs.Ticket;

namespace CentralTicket.Contexts.Profile.Interfaces.IUseCases
{
    public interface IGetTicketsByUserIdUseCase
    {
        public List<ReadTicketDTO> Run(Guid id);
    }
}
