using CentralTicket.Contexts.Auth.Data;
using CentralTicket.Contexts.Auth.Entities;
using CentralTicket.Contexts.Auth.Interfaces.IRepositories;

namespace CentralTicket.Contexts.Auth.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly AuthDbContext _database;

        public UserRepository(AuthDbContext database)
        {
            _database = database;
        }
        public List<User> GetAll()
        {
            return _database.Users.ToList();
        }
        public void Create(User newUser)
        {
            _database.Users.Add(newUser);
            _database.SaveChanges();
        }
    }
}
