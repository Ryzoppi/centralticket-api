using CentralTicket.Contexts.Profile.Data;
using CentralTicket.Contexts.Profile.Entities;
using CentralTicket.Contexts.Profile.Interfaces.IRepositories;

namespace CentralTicket.Contexts.Profile.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ProfileDbContext _database;

        public UserRepository(ProfileDbContext database)
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
