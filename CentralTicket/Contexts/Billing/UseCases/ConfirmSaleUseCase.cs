using CentralTicket.Contexts.Billing.Entities;
using CentralTicket.Contexts.Billing.Enums;
using CentralTicket.Contexts.Billing.Interfaces.IRepositories;
using CentralTicket.Contexts.Billing.Interfaces.IUseCases;

namespace CentralTicket.Contexts.Billing.UseCases
{
    public class ConfirmSaleUseCase : IConfirmSaleUseCase
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ITicketRepository _ticketRepository;

        public ConfirmSaleUseCase(ISaleRepository saleRepository, ITicketRepository ticketRepository)
        {
            _saleRepository = saleRepository;
            _ticketRepository = ticketRepository;
        }

        public void Run(Guid id)
        {
            Sale sale = this._saleRepository.GetById(id);

            foreach (Ticket ticket in sale.PurchasedTickets)
            {
                ticket.Status = TicketStatus.Sold;

                this._ticketRepository.Update(ticket);
            }

            sale.Status = SaleStatus.Approved;

            this._saleRepository.Update(sale);
        }
    }
}
