using CentralTicket.Contexts.Billing.Enums;
using CentralTicket.Contexts.Billing.ValueObjects;
using CentralTicket.Entities;

namespace CentralTicket.Contexts.Billing.Entities
{
    public class Ticket : Base
    {
        public Price Value { get; private set; }
        public Category Category {  get; set; }
        public Kind Kind { get; set; }
        public TicketStatus Status { get; set; }
        public Event Event { get; set; }
    }
}
