namespace CentralTicket.Contexts.Profile.UseCases
{
    using CentralTicket.Contexts.Profile.DTOs.Ticket;
    using CentralTicket.Contexts.Profile.Interfaces.IRepositories;
    using CentralTicket.Contexts.Profile.Interfaces.IUseCases;

    public class GetTicketsByUserIdUseCase : IGetTicketsByUserIdUseCase
    {
        private readonly ITicketRepository _repository;

        public GetTicketsByUserIdUseCase(ITicketRepository repository)
        {
            _repository = repository;
        }

        public List<ReadTicketDTO> Run(Guid userId)
        {
            var tickets = _repository.GetByUserId(userId);

            return tickets.Select(t => new ReadTicketDTO
            {
                Id = t.Id,
                Status = t.Status,
                EventTitle = t.Event?.Title,
                EventStartDate = t.Event.StartDate,
                EventEndDate = t.Event.EndDate
            }).ToList();
        }
    }
}
