using CentralTicket.Contexts.Profile.ValueObjects;
using CentralTicket.DTOs.Base;

namespace CentralTicket.Contexts.Profile.DTOs.User
{
    public class ReadUserDTO : ReadBaseDTO
    {
        public Name Name { get; set; }
        public Password Password { get; set; }
        public Email Email { get; set; }
        public string ProfilePictureUrl { get; set; }
    }
}
