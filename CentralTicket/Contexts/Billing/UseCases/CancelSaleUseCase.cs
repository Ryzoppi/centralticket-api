using CentralTicket.Contexts.Billing.Entities;
using CentralTicket.Contexts.Billing.Enums;
using CentralTicket.Contexts.Billing.Interfaces.IRepositories;
using CentralTicket.Contexts.Billing.Interfaces.IUseCases;

namespace CentralTicket.Contexts.Billing.UseCases
{
    public class CancelSaleUseCase : ICancelSaleUseCase
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ITicketRepository _ticketRepository;

        public CancelSaleUseCase(ISaleRepository saleRepository, ITicketRepository ticketRepository)
        {
            _saleRepository = saleRepository;
            _ticketRepository = ticketRepository;
        }

        public void Run(Guid id)
        {
            Sale sale = this._saleRepository.GetById(id);

            sale.Status = SaleStatus.Canceled;

            foreach(Ticket ticket in sale.PurchasedTickets)
            {
                ticket.Status = TicketStatus.Available;

                this._ticketRepository.Update(ticket);
            }

            // Reembolso

            this._saleRepository.Update(sale);
        }
    }
}
