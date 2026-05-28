using CentralTicket.Contexts.Profile.Entities;

namespace CentralTicket.Contexts.Profile.Interfaces.IRepositories
{
    public interface ITicketRepository
    {
        public List<Ticket> GetByUserId(Guid userId);
    }
}
