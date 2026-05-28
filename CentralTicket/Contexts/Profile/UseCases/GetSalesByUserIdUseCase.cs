namespace CentralTicket.Contexts.Profile.UseCases
{
    using CentralTicket.Contexts.Profile.DTOs.Sale;
    using CentralTicket.Contexts.Profile.DTOs.Ticket;
    using CentralTicket.Contexts.Profile.Interfaces.IRepositories;
    using CentralTicket.Contexts.Profile.Interfaces.IUseCases;

    public class GetSalesByUserIdUseCase : IGetSalesByUserIdUseCase
    {
        private readonly ISaleRepository _repository;

        public GetSalesByUserIdUseCase(ISaleRepository repository)
        {
            _repository = repository;
        }

        public List<ReadSaleDTO> Run(Guid userId)
        {
            var sales = _repository.GetByUserId(userId);

            return sales.Select(s => new ReadSaleDTO
            {
                Id = s.Id,
                TotalValue = s.TotalValue,
                PaymentMethod = s.PaymentMethod,
                OrderCode = s.OrderCode,
                PurchasedTickets = s.PurchasedTickets.Select(t => new ReadTicketDTO
                {
                    Id = t.Id,
                    Status = t.Status,
                    EventTitle = t.Event.Title,
                    EventStartDate = t.Event.StartDate,
                    EventEndDate = t.Event.EndDate
                }).ToList()
            }).ToList();
        }
    }
}