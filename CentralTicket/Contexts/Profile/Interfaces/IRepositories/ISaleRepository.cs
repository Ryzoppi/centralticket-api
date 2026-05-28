using CentralTicket.Contexts.Profile.Entities;

namespace CentralTicket.Contexts.Profile.Interfaces.IRepositories
{
    public interface ISaleRepository
    {
        public List<Sale> GetByUserId(Guid id);
    }
}