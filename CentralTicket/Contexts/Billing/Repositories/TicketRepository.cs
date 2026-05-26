using CentralTicket.Contexts.Billing.Data;
using CentralTicket.Contexts.Billing.Entities;
using CentralTicket.Contexts.Billing.Interfaces.IRepositories;

namespace CentralTicket.Contexts.Billing.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly BillingDbContext _database;

        public TicketRepository(BillingDbContext database)
        {
            _database = database;
        }

        public List<Ticket> GetByIds(List<Guid> ids)
        {
            List<Ticket> tickets = _database.Tickets.Where(ticket => ids.Contains(ticket.Id)).ToList();

            return tickets;
        }

        public void Update(Ticket ticket)
        {
            _database.Set<Ticket>().Update(ticket);
            _database.SaveChanges();
        }
    }
}
