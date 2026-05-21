using CentralTicket.Contexts.Billing.Enums;
using CentralTicket.Contexts.Billing.ValueObjects;
using CentralTicket.Entities;

namespace CentralTicket.Contexts.Billing.Entities
{
    public class Sale : Base
    {
        public Price TotalValue { get; private set; }
        public PaymentMethod PaymentMethod {  get; set; }
        public SaleStatus Status { get; set; }
        public OrderCode OrderCode { get; set; }
        public User Customer { get; set; }
        public List<Ticket> PurchasedTickets { get; set; }

        public void UpdateTotalValue(Price totalValue)
        {
            this.TotalValue = totalValue;
        }
    }
}
