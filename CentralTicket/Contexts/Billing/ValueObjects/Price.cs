namespace CentralTicket.Contexts.Billing.ValueObjects
{
    public class Price
    {
        public decimal Value { get; private set; }

        public Price(decimal price)
        {
            if (price < 0) throw new Exception("Valor deve ser positivo");

            this.Value = price;
        }
    }
}
