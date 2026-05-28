
using CentralTicket.Entities;

namespace CentralTicket.Contexts.Profile.Entities
{
    public class Event : Base
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TicketAmount { get; set; }
    }
}
