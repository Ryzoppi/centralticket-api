using CentralTicket.Entities;

namespace CentralTicket.Contexts.Profile.Entities
{
    public class Sale : Base
    {
        public double TotalValue { get; private set; }
        public string PaymentMethod { get; set; }
        public string OrderCode { get; set; }
        public User Customer { get; set; }
        public List<Ticket> PurchasedTickets { get; set; }

    }
}