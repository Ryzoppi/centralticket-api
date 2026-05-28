using CentralTicket.Contexts.Profile.DTOs.Ticket;

namespace CentralTicket.Contexts.Profile.DTOs.Sale
{
    public class ReadSaleDTO
    {
        public Guid Id { get; set; }
        public double TotalValue { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public string OrderCode { get; set; }
        public List<ReadTicketDTO> PurchasedTickets { get; set; }
    }
}
