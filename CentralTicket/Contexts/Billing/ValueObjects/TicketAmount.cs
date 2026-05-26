namespace CentralTicket.Contexts.Billing.ValueObjects
{
    public class TicketAmount
    {
        public int Value { get; private set; }

        public TicketAmount(int ticketAmount)
        {
            if (ticketAmount < 0) throw new Exception("Quantidade de ingressos deve ser positiva");

            this.Value = ticketAmount;
        }

        private TicketAmount() { }
    }
}
