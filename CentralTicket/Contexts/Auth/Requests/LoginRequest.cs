using CentralTicket.Contexts.Auth.ValueObjects;

namespace CentralTicket.Contexts.Auth.Requests
{
    public class LoginRequest
    {
        public required Email Email { get; set; }
        public required Password Password { get; set; }
    }
}
