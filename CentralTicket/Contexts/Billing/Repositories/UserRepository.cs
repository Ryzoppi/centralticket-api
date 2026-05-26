using CentralTicket.Contexts.Billing.Data;
using CentralTicket.Contexts.Billing.Entities;
using CentralTicket.Contexts.Billing.Interfaces.IRepositories;

namespace CentralTicket.Contexts.Billing.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BillingDbContext _database;

        public UserRepository(BillingDbContext database)
        {
            _database = database;
        }

        public User GetById(Guid id)
        {
            User user = _database.Users.Select(user => user).Where(user => user.Id == id).FirstOrDefault();

            return user;
        }
    }
}
