namespace CentralTicket.Contexts.Profile.DTOs.Ticket
{
    public class ReadTicketDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string EventTitle { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
    }
}
