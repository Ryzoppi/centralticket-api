using CentralTicket.Contexts.Auth.ValueObjects;

namespace CentralTicket.Contexts.Auth.Requests
{
    public class RegisterRequest
    {
        public required Name Name { get; set; }
        public required Email Email { get; set; }
        public required Password Password { get; set; } 

    }
}
