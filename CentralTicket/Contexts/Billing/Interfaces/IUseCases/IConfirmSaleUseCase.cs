namespace CentralTicket.Contexts.Billing.Interfaces.IUseCases
{
    public interface IConfirmSaleUseCase
    {
        public void Run(Guid id);
    }
}
