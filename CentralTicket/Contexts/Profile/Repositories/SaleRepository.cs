// Profile/Repositories/SaleRepository.cs
namespace CentralTicket.Contexts.Profile.Repositories
{
    using CentralTicket.Contexts.Profile.Data;
    using CentralTicket.Contexts.Profile.Entities;
    using CentralTicket.Contexts.Profile.Interfaces.IRepositories;
    using Microsoft.EntityFrameworkCore;

    public class SaleRepository : ISaleRepository
    {
        private readonly ProfileDbContext _database;

        public SaleRepository(ProfileDbContext database)
        {
            _database = database;
        }

        public List<Sale> GetByUserId(Guid userId)
        {
            return _database.Sales
                .Include(s => s.PurchasedTickets)
                    .ThenInclude(t => t.Event)
                .Where(s => s.Customer.Id == userId)
                .ToList();
        }
    }
}