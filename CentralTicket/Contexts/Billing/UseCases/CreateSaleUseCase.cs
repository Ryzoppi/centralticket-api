using CentralTicket.Contexts.Billing.DTOs.Sale;
using CentralTicket.Contexts.Billing.Entities;
using CentralTicket.Contexts.Billing.Enums;
using CentralTicket.Contexts.Billing.Interfaces.IRepositories;

namespace CentralTicket.Contexts.Billing.UseCases
{
    public class CreateSaleUseCase
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITicketRepository _ticketRepository;

        public CreateSaleUseCase(ISaleRepository saleRepository, IUserRepository userRepository, ITicketRepository ticketRepository)
        {
            _saleRepository = saleRepository;
            _userRepository = userRepository;
            _ticketRepository = ticketRepository;
        }

        public void Run(CreateSaleDTO sale)
        {
            User customer = this._userRepository.GetById(sale.UserId);

            List<Ticket> tickets = this._ticketRepository.GetByIds(sale.TicketIds);

            Sale newSale = new Sale{
                PaymentMethod = sale.PaymentMethod,
                Customer = customer,
                PurchasedTickets = tickets,
            };

            newSale.UpdateTotalValue(sale.TotalValue);
            newSale.Status = SaleStatus.AwaitingApproval;

            this._saleRepository.Create(newSale);

            foreach (Ticket ticket in tickets)
            {
                ticket.Status = TicketStatus.Reserved;

                this._ticketRepository.Update(ticket);
            }
        }
    }
}
