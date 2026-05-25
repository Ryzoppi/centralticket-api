using CentralTicket.Contexts.Auth.ValueObjects;

namespace CentralTicket.Contexts.Auth.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public Name Name { get; set; }
        public string PasswordHash { get; set; }
        public Email Email { get; set; }
        public string ProfilePictureUrl { get; set; } = string.Empty;
        public List<Sale> Sales { get; set; } = new List<Sale>();
        public DateOnly createdAt { get; set; }
    }
}
