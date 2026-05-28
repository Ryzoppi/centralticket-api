using CentralTicket.Entities;

namespace CentralTicket.Contexts.Profile.Entities
{
    public class Ticket : Base
    {
        public double Value { get; private set; }
        public string Category {  get; set; }
        public string Kind { get; set; }
        public string Status { get; private set; }
        public Event Event { get; set; }
    }
}
