using CentralTicket.Entities;

namespace CentralTicket.Contexts.Profile.Entities
{
    public class User : Base
    {
        public int Id { get; set; }
        public Name Name { get; set; }
        public Email Email { get; set; }
        public DateOnly createdAt { get; set; }
    }
}
