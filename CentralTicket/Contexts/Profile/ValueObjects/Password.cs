using CentralTicket.Entities;

namespace CentralTicket.Contexts.Profile.ValueObjects
{
    public class Password 
    {
        public string Value { get; private set; }

        public Password(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (value.Length < 6) throw new Exception("A senha deve conter no mínimo 6 caracteres");

            this.Value = value;
        }
        private Password() { }
    }
}
