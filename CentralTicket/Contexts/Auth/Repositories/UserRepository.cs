using CentralTicket.Contexts.Auth.Data;
using CentralTicket.Contexts.Auth.Entities;
using CentralTicket.Contexts.Auth.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CentralTicket.Contexts.Auth.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly Context _database;

        public UserRepository(Context database)
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
