namespace CentralTicket.Contexts.Billing.ValueObjects
{
    public class OrderCode
    {
        public string Value { get; private set; }

        public OrderCode(string orderCode)
        {
            if (orderCode == null) throw new ArgumentNullException("Título precisa ser preenchido");

            this.Value = orderCode;
        }

        private OrderCode() { }
    }
}
