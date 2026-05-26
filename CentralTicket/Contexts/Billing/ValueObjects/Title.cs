namespace CentralTicket.Contexts.Billing.ValueObjects
{
    public class Title
    {
        public string Value { get; private set; }

        public Title(string title)
        {
            if (title == null) throw new ArgumentNullException("Título precisa ser preenchido");

            if (title.Length < 2) throw new Exception("Título deve ter pelo menos 2 caractéres");

            this.Value = title;
        }

        private Title() { }
    }
}
