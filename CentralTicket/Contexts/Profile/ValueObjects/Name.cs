namespace CentralTicket.Contexts.Profile.ValueObjects
{
    public class Name 
    {
        public string Value { get; private set; }
        public Name(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (value.Length < 3) throw new Exception("O nome deve conter no mínimo 3 caracteres");
            this.Value = value;
        }
        private Name() { }
    }
}
