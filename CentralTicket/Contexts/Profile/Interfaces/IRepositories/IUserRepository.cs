using CentralTicket.Contexts.Profile.Entities;

namespace CentralTicket.Contexts.Profile.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        public User GetById(Guid id);
    }
}
