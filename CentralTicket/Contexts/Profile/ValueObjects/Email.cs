namespace CentralTicket.Contexts.Profile.ValueObjects
{
    public class Email
    {
        public string Value { get; private set; }

        public Email(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            this.Value = value;
        }
        private Email() { }
    }
}
