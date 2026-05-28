using CentralTicket.Contexts.Auth.Entities;

namespace CentralTicket.Contexts.Auth.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        public List<User> GetAll();
        public void Create(User newSale);
        public void SaveChanges();
    }
}
