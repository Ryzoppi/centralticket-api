using CentralTicket.Contexts.Billing.Enums;
using CentralTicket.Contexts.Billing.ValueObjects;

namespace CentralTicket.Contexts.Billing.DTOs.Sale
{
    public class CreateSaleDTO
    {
        public Price TotalValue { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Guid UserId { get; set; }
        public List<Guid> TicketIds { get; set; }
    }
}